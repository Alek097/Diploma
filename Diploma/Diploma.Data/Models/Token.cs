using System;

namespace Diploma.Data.Models
{
    public class Token : BaseEntity<Guid>
    {
        public string AccessToken { get; set; }

        public string Code { get; set; }

        public string Provider { get; set; }

        public string UserProviderId { get; set; }
    }
}