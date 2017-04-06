using System;
using System.Collections.Generic;

namespace Diploma.Data.Models
{
    public class Category : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }

        public Category()
        {
            this.Products = new List<Product>();
            this.Id = Guid.NewGuid();
        }
    }
}
