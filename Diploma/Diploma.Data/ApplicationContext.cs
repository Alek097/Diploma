using Diploma.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Diploma.Data
{
    public class ApplicationContext : IdentityDbContext<User>, IContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CharacteristicsGroup> CharacteristicsGroups { get; set; }

        public DbSet<Characteristic> Characteristics { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Ban> Bans { get; set; }

        public DbSet<OAuthState> OAuthStates { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await Task.Run<int>(() => this.SaveChanges());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=(LocalDb)\v11.0;Initial Catalog=Diploma;Integrated Security=True;");
        }
    }
}
