using Diploma.Data.Models;
using System;

namespace Diploma.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
