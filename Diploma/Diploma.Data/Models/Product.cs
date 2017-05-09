using System;
using System.Collections.Generic;

namespace Diploma.Data.Models
{
    public class Product : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string CoverUrl { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<CharacteristicsGroup> CharacteristicsGroups { get; set; }

        public virtual ICollection<Characteristic> Characteristics { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual Guid? OrderId { get; set; }

        public Product()
        {
            this.CharacteristicsGroups = new List<CharacteristicsGroup>();
            this.Characteristics = new List<Characteristic>();
            this.Images = new List<Image>();
            this.Id = Guid.NewGuid();
        }
    }
}