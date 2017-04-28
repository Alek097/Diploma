using System;
using System.Collections.Generic;
using System.Text;

namespace Diploma.Data.Models
{
    public class EditEmailConfirmMessage : BaseEntity<Guid>
    {
        public string SecretCode { get; set; }

        public EditEmailConfirmMessage()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
