using System;
using System.Collections.Generic;

namespace Diploma.Data.Models
{
    public class Order : BaseEntity<Guid>
    {
        public decimal TotalPrice { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Order()
        {
            this.Products = new List<Product>();
            this.Id = Guid.NewGuid();
        }
    }
}