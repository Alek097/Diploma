using Diploma.Data.Models;
using System;
using System.Collections.Generic;
using Diploma.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Diploma.Repositories.Interfaces;

namespace Diploma.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(IContext context) : base(context)
        {
        }

        public override IList<Product> Get()
        {
            return base.context.Products
                .Include(p => p.CharacteristicsGroups)
                .Include(p => p.Characteristics)
                .Include(p => p.Images)
                .ToList();
        }
    }
}
