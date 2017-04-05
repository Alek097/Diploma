using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Diploma.Data.Models
{
    public class User : IdentityUser
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

        public DateTime? BirthDate { get; set; }

        public bool IsBanned { get; set; }

        public virtual ICollection<Ban> Bans { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public User()
        {
            this.Bans = new List<Ban>();
            this.Orders = new List<Order>();
        }

    }
}
