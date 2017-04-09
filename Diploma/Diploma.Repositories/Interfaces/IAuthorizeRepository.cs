using Diploma.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Diploma.Repositories.Interfaces
{
    public interface IAuthorizeRepository : IDisposable
    {
        Task<string> GetRedirectUrl(string provider);
        Task<List<OAuthViewModel>> GetOAuthProviders();
    }
}
