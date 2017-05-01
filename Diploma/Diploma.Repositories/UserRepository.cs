using Diploma.Data;
using Diploma.Data.Models;
using Diploma.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diploma.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(IContext context) : base(context)
        {
        }

        public override IList<User> Get()
        {
            return this.context.Users
                .Include(u => u.Addresses)
                .Include(u => u.Orders)
                .Include(u => u.Roles)
                .ToList();
        }
    }
}
