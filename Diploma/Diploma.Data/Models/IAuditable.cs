using System;

namespace Diploma.Data.Models
{
    public interface IAuditable
    {
        DateTime CreateDate { get; set; }
        Guid CreateBy { get; set; }
        DateTime LastModifyDate { get; set; }
        Guid ModifyBy { get; set; }
    }
}
