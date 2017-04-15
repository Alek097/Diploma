using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.Data.Models
{
    public class Address : BaseEntity<Guid>
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [NotMapped]
        public string FullName { get => $"{this.LastName} {this.FirstName} {this.MiddleName}"; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string PostCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}