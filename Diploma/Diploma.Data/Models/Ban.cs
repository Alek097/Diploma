using System;

namespace Diploma.Data.Models
{
    public class Ban : BaseEntity<Guid>
    {
        public string Cause { get; set; }

        public Ban()
        {
            this.Id = Guid.NewGuid();
        }
    }
}