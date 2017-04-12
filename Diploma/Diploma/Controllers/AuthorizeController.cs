using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Diploma.Repositories.Interfaces;
using Diploma.Core.ViewModels;
using Microsoft.Extensions.Logging;
using Diploma.Core.ConfigureModels;
using Microsoft.Extensions.Options;

namespace Diploma.Controllers
{
    [Produces("application/json")]
    [Route("api/Authorize/[action]")]
    public class AuthorizeController : Controller
    {
        private readonly IAuthorizeRepository repository;
        private readonly ILogger<AuthorizeController> logger;
        private readonly App app;

        public AuthorizeController(IAuthorizeRepository repository, ILogger<AuthorizeController> logger, IOptions<App> app)
        {
            this.repository = repository;
            this.logger = logger;
            this.app = app.Value;
        }

        [HttpGet]
        [Produces("text/plain")]
        public async Task<string> GetRedirectUrl(string provider)
        {
            return await this.repository.GetRedirectUrl(provider);
        }

        [HttpGet]
        public async Task<List<OAuthViewModel>> GetOAuthProviders()
        {
            return await this.repository.GetOAuthProviders();
        }

        [HttpGet]
        public async Task<RedirectResult> SetCode(string code, string state)
        {
            string url = null;

            try
            {
                url = await this.repository.SetAccessCode(code, state);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message, ex);

                url = $"{this.app.Domain}#!/error/500/BadRequest";
            }

            return this.Redirect(url);
        }

        [HttpGet]
        public async Task<UserViewModel> GetUser()
        {
            return User.Identity.IsAuthenticated ? await this.repository.GetUser(User.Identity.Name) : new UserViewModel() { IsAuthorize = false };
        }

        [HttpGet]
        public async Task SignOut()
        {
            await this.repository.SignOut();
        }

        public new void Dispose()
        {
            this.repository.Dispose();
            base.Dispose();
        }
    }
}