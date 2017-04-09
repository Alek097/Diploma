using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Diploma.Repositories.Interfaces;
using Diploma.Core.ViewModels;

namespace Diploma.Controllers
{
    [Produces("application/json")]
    [Route("api/Authorize/[action]")]
    public class AuthorizeController : Controller
    {
        private readonly IAuthorizeRepository repository;

        public AuthorizeController(IAuthorizeRepository repository)
        {
            this.repository = repository;
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
        public async Task SetCode(string code, string state)
        {
            throw new NotImplementedException();
        }

        public new void Dispose()
        {
            this.repository.Dispose();
            base.Dispose();
        }
    }
}