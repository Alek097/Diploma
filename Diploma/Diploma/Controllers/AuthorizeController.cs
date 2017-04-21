using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Diploma.Repositories.Interfaces;
using Diploma.Core.ViewModels;
using Microsoft.Extensions.Logging;
using Diploma.Core.ConfigureModels;
using Microsoft.Extensions.Options;
using Diploma.Core;
using Diploma.Filters;

namespace Diploma.Controllers
{
    [Produces("application/json")]
    [Route("api/Authorize/[action]")]
    [TypeFilter(typeof(ExceptionFilterAttribute))]
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
        [TypeFilter(typeof(RedirectExceptionFilterAttribute))]
        public async Task<RedirectResult> SetCode(string code, string state)
        {
            string url = await this.repository.SetAccessCode(code, state);
            return this.Redirect(url);
        }

        [HttpGet]
        public async Task<ControllerResult<UserViewModel>> GetUser()
        {
            return User.Identity.IsAuthenticated ?
                await this.repository.GetUser(User.Identity.Name) :
                new ControllerResult<UserViewModel>()
                {
                    IsSuccess = true,
                    Status = 200,
                    Value = new UserViewModel() { IsAuthorize = false }
                };

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