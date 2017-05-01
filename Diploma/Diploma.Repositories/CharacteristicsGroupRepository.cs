using Diploma.Data.Models;
using System;
using System.Collections.Generic;
using Diploma.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Diploma.Repositories.Interfaces;

namespace Diploma.Repositories
{
    public class CharacteristicsGroupRepository : Repository<CharacteristicsGroup, Guid>, ICharacteristicsGroupRepository
    {
        public CharacteristicsGroupRepository(IContext context) : base(context)
        {
        }

        public override IList<CharacteristicsGroup> Get()
        {
            return base.context.CharacteristicsGroups
                .Include(chg => chg.Characteristics)
                .ToList();
        }
    }
}
