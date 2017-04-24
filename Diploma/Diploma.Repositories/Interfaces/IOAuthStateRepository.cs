using Diploma.Data.Models;
using System;

namespace Diploma.Repositories.Interfaces
{
    public interface IOAuthStateRepository : IRepository<OAuthState, Guid>
    {
    }
}
