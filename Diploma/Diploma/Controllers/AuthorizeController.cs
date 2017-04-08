using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Diploma.Repositories.Interfaces;

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

        public async Task<string> GetRedirectUrl(string provider)
        {
            return await this.repository.GetRedirectUrl(provider);
        }

        public new void Dispose()
        {
            this.repository.Dispose();
            base.Dispose();
        }
    }
}