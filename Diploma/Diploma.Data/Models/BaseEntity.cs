using System;
using System.Collections.Generic;
using System.Text;

namespace Diploma.Data.Models
{
    public class BaseEntity<T> : IBaseEntity<T>, IAuditable, IDeletable
    {
        public T Id { get; set; }

        public DateTime? CreateDate { get; set; }

        public Guid? CreateBy { get; set; }

        public DateTime? LastModifyDate { get; set; }

        public Guid? ModifyBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
