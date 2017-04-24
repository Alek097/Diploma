using Diploma.Data.Models;
using System;
using System.Collections.Generic;
using Diploma.Data;
using System.Linq;
using Diploma.Repositories.Interfaces;

namespace Diploma.Repositories
{
    public class BanRepository : Repository<Ban, Guid>, IBanRepository
    {
        public BanRepository(IContext context) : base(context)
        {
        }

        public override IList<Ban> Get()
        {
            return base.context.Bans.ToList();
        }
    }
}
