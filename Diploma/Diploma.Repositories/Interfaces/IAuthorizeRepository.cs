using Diploma.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Repositories.Interfaces
{
    public interface IAuthorizeRepository : IDisposable
    {
        Task<string> GetRedirectUrl(string provider);
        Task<List<OAuthViewModel>> GetOAuthProviders();
        Task<string> SetAccessCode(string code, string state);
    }
}
