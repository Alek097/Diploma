using System;

namespace Diploma.Data.Models
{
    public class Image : BaseEntity<Guid>
    {
        public string Url { get; set; }

        public Image()
        {
            this.Id = Guid.NewGuid();
        }
    }
}