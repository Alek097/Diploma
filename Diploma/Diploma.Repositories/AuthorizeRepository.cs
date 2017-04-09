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

namespace Diploma.Repositories
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private readonly IContext context;
        private readonly List<OAuth> oauth;
        private readonly App app;

        private const string setCodeUrl = "api/Authorize/SetCode";

        public AuthorizeRepository(IContext context, IOptions<List<OAuth>> oauth, IOptions<App> app)
        {
            this.context = context;
            this.oauth = oauth.Value;
            this.app = app.Value;
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

            string state = $"{OAuth.Name.ToUpper()}_{Guid.NewGuid()}";

            if (this.app.Domain[this.app.Domain.Length - 1] != '/')
            {
                this.app.Domain += '/';
            }

            string redirectUrl = $"{this.app.Domain}{setCodeUrl}";

            this.context.OAuthStates.Add(new OAuthState()
            {
                State = state
            });

            await this.context.SaveChangesAsync();

            return $"{string.Format(OAuth.Url, OAuth.Client_id, redirectUrl)}&{OAuth.Parameters}&state={state}";
        }
    }
}
