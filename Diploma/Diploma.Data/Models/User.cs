using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;

namespace Diploma.Data.Models
{
    public class User : IdentityUser, IAuditable
    {
        [NotMapped]
        public bool IsBanned
        {
            get
            {
                Ban lastBan = this.Bans.FirstOrDefault((b) => !(b.IsDeleted));

                return lastBan != null;
            }
        }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? LastModifyDate { get; set; }

        public string ModifyBy { get; set; }

        public virtual ICollection<Ban> Bans { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }

        public User()
        {
            this.Bans = new List<Ban>();
            this.Orders = new List<Order>();
            this.Tokens = new List<Token>();
            this.Addresses = new List<Address>();
        }

    }
}
