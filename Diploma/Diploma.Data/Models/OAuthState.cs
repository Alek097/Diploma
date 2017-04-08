using System;
using System.Collections.Generic;
using System.Text;

namespace Diploma.Data.Models
{
    public class OAuthState : IBaseEntity<Guid>, IDeletable
    {
        public Guid Id { get; set; }

        public string State { get; set; }

        public bool IsDeleted { get; set; }

        public OAuthState()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
