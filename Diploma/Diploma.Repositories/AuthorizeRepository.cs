using Diploma.Core.ConfigureModels;
using Diploma.Data;
using Diploma.Data.Models;
using Diploma.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diploma.Core.ViewModels;
using System.Net;
using System.IO;
using Diploma.Core.OAuthResults;
using Microsoft.AspNetCore.Identity;

namespace Diploma.Repositories
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private readonly IContext context;
        private readonly List<OAuth> oauth;
        private readonly App app;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private const string url = "api/Authorize/SetCode";

        public AuthorizeRepository(IContext context, IOptions<List<OAuth>> oauth, IOptions<App> app, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.context = context;
            this.oauth = oauth.Value;
            this.app = app.Value;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public async Task<List<OAuthViewModel>> GetOAuthProviders()
        {
            return await Task.Run(() =>
            {
                return this.oauth.Select((oa) => new OAuthViewModel()
                {
                    Name = oa.Name,
                    Description = oa.Description,
                    LogoUrl = oa.LogoUrl
                })
                .ToList();
            });
        }

        public async Task<string> GetRedirectUrl(string provider)
        {
            OAuth OAuth = this.oauth.First((oa) => oa.Name.ToUpper() == provider.ToUpper());

            string state = Guid.NewGuid().ToString();

            string redirectUrl = $"{this.app.Domain}{url}";

            this.context.Create<OAuthState, Guid>(new OAuthState()
            {
                State = state,
                Provider = provider
            });

            await this.context.SaveChangesAsync();

            return $"{string.Format(OAuth.GetCodeUrl, OAuth.ClientId, redirectUrl)}&{OAuth.GetCodeParameters}&state={state}";
        }

        public async Task<string> SetAccessCode(string code, string state)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Code is null or empty", "code");
            }
            else if (string.IsNullOrWhiteSpace(state))
            {
                throw new ArgumentException("State is null or empty", "state");
            }

            OAuthState stateEntity;

            try
            {
                stateEntity = this.context.OAuthStates.First((s) => s.State == state);
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgumentException("Invalid state", "state", ex);
            }

            OAuth provider = this.oauth.FirstOrDefault((p) => p.Name.ToUpper() == stateEntity.Provider.ToUpper());

            this.context.Delete(stateEntity);

            await this.context.SaveChangesAsync();

            string redirectUrl = $"{this.app.Domain}{url}";

            string getAccessCodeUrl = $"{string.Format(provider.GetAccessTokenUrl, provider.ClientId, provider.ClientSecret, redirectUrl, code)}&{provider.GetAccessTokenParameters}";

            WebRequest getAccessCodeRequest = WebRequest.Create(getAccessCodeUrl);
            WebResponse getAccessCodeResponce = await getAccessCodeRequest.GetResponseAsync();

            string json;

            using (Stream stream = getAccessCodeResponce.GetResponseStream())
            {
                StreamReader streamReader = new StreamReader(stream);

                json = streamReader.ReadToEnd();
            }

            OAuthResult oResult = null;

            switch (provider.Name.ToUpper())
            {
                case "VK":
                    oResult = new VkOAuthResult(json).ToOAuthResult();
                    break;

                default:
                    throw new InvalidOperationException($"Provider \"{provider.Name}\" not a found");
            }

            User user = this.context.Users.FirstOrDefault(u => u.Email == oResult.Email);

            if (user == null)
            {
                user = new User()
                {
                    Email = oResult.Email,
                    UserName = oResult.Email
                };

                user.Tokens.Add(new Token()
                {
                    Provider = oResult.Provider,
                    AccessToken = oResult.AccessToken,
                    UserProviderId = oResult.UserId,
                    Code = code
                });

                await this.userManager.CreateAsync(user);
            }

            await this.signInManager.SignInAsync(user, false);

            return $"/";
        }
    }
}
