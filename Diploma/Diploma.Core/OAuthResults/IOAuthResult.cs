using System.Threading.Tasks;

namespace Diploma.Core.OAuthResults
{
    public interface IOAuthResult
    {
        Task<OAuthResult> ToOAuthResultAsync();
    }
}
