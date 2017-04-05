using System;
using System.Collections.Generic;

namespace Diploma.Data.Models
{
    public class CharacteristicsGroup : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public virtual ICollection<Characteristic> Characteristics { get; set; }

        public CharacteristicsGroup()
        {
            this.Characteristics = new List<Characteristic>();
            this.Id = Guid.NewGuid();
        }
    }
}