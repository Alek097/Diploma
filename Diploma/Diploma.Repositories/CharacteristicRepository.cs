using Diploma.Data.Models;
using System;
using System.Collections.Generic;
using Diploma.Data;
using System.Linq;
using Diploma.Repositories.Interfaces;

namespace Diploma.Repositories
{
    public class CharacteristicRepository : Repository<Characteristic, Guid>, ICharacteristicRepository
    {
        public CharacteristicRepository(IContext context) : base(context)
        {
        }

        public override IList<Characteristic> Get()
        {
            return base.context.Characteristics.ToList();
        }
    }
}
