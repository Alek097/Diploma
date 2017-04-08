using Diploma.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Diploma.Data
{
    public interface IContext : IDisposable
    {
        DbSet<Category> Categories { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<CharacteristicsGroup> CharacteristicsGroups { get; set; }

        DbSet<Characteristic> Characteristics { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<Ban> Bans { get; set; }

        DbSet<OAuthState> OAuthStates { get; set; }

        Task<int> SaveChangesAsync();

        int SaveChanges();
    }
}
