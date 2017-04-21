using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Diploma.Core;
using Diploma.Core.ViewModels;

namespace Diploma.Controllers
{
    [Produces("application/json")]
    [Route("api/Profile/[action]")]
    [Authorize]
    public class ProfileController : Controller
    {
        public Task<ControllerResult> Edit(UserViewModel user)
        {
            throw new NotImplementedException();
        }
    }
}