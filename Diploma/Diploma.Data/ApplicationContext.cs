﻿using Diploma.Core.ConfigureModels;
using Diploma.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Diploma.Data
{
    public class ApplicationContext : IdentityDbContext<User>, IContext
    {
        private readonly App app;

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CharacteristicsGroup> CharacteristicsGroups { get; set; }

        public DbSet<Characteristic> Characteristics { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Ban> Bans { get; set; }

        public DbSet<OAuthState> OAuthStates { get; set; }

        public ApplicationContext(IOptions<App> app)
        {
            this.app = app.Value;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Task.Run<int>(() => this.SaveChanges());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"data source=(LocalDb)\v11.0;Initial Catalog=Diploma;Integrated Security=True;");
            optionsBuilder.UseSqlServer(this.app.ConnectionString);
        }
    }
}
