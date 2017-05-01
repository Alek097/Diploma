using Diploma.Data.Models;
using System;
using System.Collections.Generic;
using Diploma.Data;
using System.Linq;
using Diploma.Repositories.Interfaces;

namespace Diploma.Repositories
{
    public class AddressRepository : Repository<Address, Guid>, IAddressRepository
    {
        public AddressRepository(IContext context) : base(context)
        {
        }

        public override IList<Address> Get()
        {
            return base.context.Addresses.ToList();
        }
    }
}
