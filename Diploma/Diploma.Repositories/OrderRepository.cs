using Diploma.Data.Models;
using System;
using System.Collections.Generic;
using Diploma.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Diploma.Repositories.Interfaces;

namespace Diploma.Repositories
{
    public class OrderRepository : Repository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(IContext context) : base(context)
        {
        }

        public override IList<Order> Get()
        {
            return base.context.Orders
                .Include(o => o.Products)
                .ToList();
        }
    }
}
