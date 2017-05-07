using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Diploma.Core;
using Diploma.Filters;
using Diploma.BusinessLogic.Interfaces;
using Diploma.Core.ViewModels;
using System;

namespace Diploma.Controllers
{
    [Produces("application/json")]
    [Route("api/Profile/[action]")]
    [Authorize]
    [TypeFilter(typeof(ExceptionFilterAttribute))]
    public class ProfileController : Controller
    {
        private readonly IProfileService profile;

        public ProfileController(IProfileService profile)
        {
            this.profile = profile;
        }

        [HttpGet]
        public async Task<ControllerResult> SendConfirmEditEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
            {
                return new ControllerResult()
                {
                    IsSuccess = false,
                    Message = "Поле email пустое.",
                    Status = 400
                };
            }
            else
            {
                return await this.profile.SendConfirmEditEmail(this.User.Identity.Name, newEmail);
            }
        }

        [HttpGet]
        public async Task<ControllerResult<string>> EditEmail(string code, string newEmail)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(newEmail))
            {
                return new ControllerResult<string>()
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Status = 400
                };
            }
            else
            {
                return await this.profile.EditEmail(code, newEmail, this.User.Identity.Name);
            }
        }

        [HttpPost]
        public async Task<ControllerResult<AddressViewModel>> AddAddress([FromBody]AddressViewModel address)
        {
            return await this.profile.AddAddress(address, this.User.Identity.Name);
        }

        [HttpGet]
        public async Task<ControllerResult> DeleteAddress(string id)
        {
            return await this.profile.DeleteAddress(id, this.User.Identity.Name);
        }

        [HttpPost]
        public async Task<ControllerResult<AddressViewModel>> EditAddress([FromBody]AddressViewModel address)
        {
            return await this.profile.EditAddress(this.User.Identity.Name, address as AddressViewModel);
        }

        [HttpGet]
        [Produces("text/plain")]
        public string GetId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}