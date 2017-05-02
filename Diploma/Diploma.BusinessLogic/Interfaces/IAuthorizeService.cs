using Diploma.Core;
using Diploma.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.BusinessLogic.Interfaces
{
    public interface IAuthorizeService : IDisposable
    {
        Task<string> GetRedirectUrl(string provider);
        Task<List<OAuthViewModel>> GetOAuthProviders();
        Task<string> SetAccessCode(string code, string state);
        Task<ControllerResult<UserViewModel>> GetUser(string name);
        Task SignOut();
    }
}
