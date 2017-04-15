using System;

namespace Diploma.Data.Models
{
    public interface IAuditable
    {
        DateTime? CreateDate { get; set; }
        string CreateBy { get; set; }
        DateTime? LastModifyDate { get; set; }
        string ModifyBy { get; set; }
    }
}
