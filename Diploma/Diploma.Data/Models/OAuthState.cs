using System;
using System.Collections.Generic;
using System.Text;

namespace Diploma.Data.Models
{
    public class OAuthState : BaseEntity<Guid>
    {
        public string State { get; set; }

        public string Provider { get; set; }

        public OAuthState()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
