using System;

namespace Diploma.Data.Models
{
    public class Ban : IAuditable, IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime LastModifyDate { get; set; }
        public Guid ModifyBy { get; set; }

        public string Cause { get; set; }
        public bool IsActive { get; set; }

        public Ban()
        {
            this.Id = Guid.NewGuid();
        }
    }
}