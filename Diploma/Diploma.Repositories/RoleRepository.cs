using Diploma.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Diploma.Data;
using System.Collections.Generic;
using Diploma.Repositories.Interfaces;
using System.Linq;

namespace Diploma.Repositories
{
    public class RoleRepository : Repository<Role, Guid>, IRoleRepository
    {
        public RoleRepository(IContext context) : base(context)
        {
        }

        public override IList<Role> Get()
        {
            return base.context.Roles.ToList();
        }
    }
}
