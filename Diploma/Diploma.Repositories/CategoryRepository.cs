using Diploma.Data.Models;
using System;
using System.Collections.Generic;
using Diploma.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Diploma.Repositories.Interfaces;

namespace Diploma.Repositories
{
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(IContext context) : base(context)
        {
        }

        public override IList<Category> Get()
        {
            return base.context.Categories
                .Include(c => c.Products)
                .ToList();
        }
    }
}
