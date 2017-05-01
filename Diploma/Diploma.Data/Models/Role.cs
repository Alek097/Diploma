using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diploma.Data.Models
{
    public class Role : IdentityRole<Guid>, IBaseEntity<Guid>
    {
        public Role()
        {

        }

        public Role(string roleName)
            :base(roleName)
        {

        }
    }
}
