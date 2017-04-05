using System;

namespace Diploma.Data.Models
{
    public class Characteristic : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public Characteristic()
        {
            this.Id = Guid.NewGuid();
        }
    }
}