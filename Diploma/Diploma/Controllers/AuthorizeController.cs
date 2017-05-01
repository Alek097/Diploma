using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Diploma.Core.ViewModels;
using Microsoft.Extensions.Logging;
using Diploma.Core.ConfigureModels;
using Microsoft.Extensions.Options;
using Diploma.Core;
using Diploma.Filters;
using Diploma.BusinessLogic.Interfaces;

namespace Diploma.Controllers
{
    [Produces("application/json")]
    [Route("api/Authorize/[action]")]
    [TypeFilter(typeof(ExceptionFilterAttribute))]
    public class AuthorizeController : Controller
    {
        private readonly IAuthorizeBusinessLogic businessLogic;
        private readonly ILogger<AuthorizeController> logger;
        private readonly App app;

        public AuthorizeController(IAuthorizeBusinessLogic businessLogic, ILogger<AuthorizeController> logger, IOptions<App> app)
        {
            this.businessLogic = businessLogic;
            this.logger = logger;
            this.app = app.Value;
        }

        [HttpGet]
        [Produces("text/plain")]
        public async Task<string> GetRedirectUrl(string provider)
        {
            return await this.businessLogic.GetRedirectUrl(provider);
        }

        [HttpGet]
        public async Task<List<OAuthViewModel>> GetOAuthProviders()
        {
            return await this.businessLogic.GetOAuthProviders();
        }

        [HttpGet]
        [TypeFilter(typeof(RedirectExceptionFilterAttribute))]
        public async Task<RedirectResult> SetCode(string code, string state)
        {
            string url = await this.businessLogic.SetAccessCode(code, state);
            return this.Redirect(url);
        }

        [HttpGet]
        public async Task<ControllerResult<UserViewModel>> GetUser()
        {
            return User.Identity.IsAuthenticated ?
                await this.businessLogic.GetUser(User.Identity.Name) :
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
            await this.businessLogic.SignOut();
        }

        public new void Dispose()
        {
            this.businessLogic.Dispose();
            base.Dispose();
        }
    }
}