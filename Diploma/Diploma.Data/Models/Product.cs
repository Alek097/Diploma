using System;
using System.Collections.Generic;

namespace Diploma.Data.Models
{
    public class Product : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoPath { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<CharacteristicsGroup> CharacteristicsGroups { get; set; }

        public virtual ICollection<Characteristic> Characteristics { get; set; }

        public Product()
        {
            this.CharacteristicsGroups = new List<CharacteristicsGroup>();
            this.Characteristics = new List<Characteristic>();
            this.Id = Guid.NewGuid();
        }
    }
}