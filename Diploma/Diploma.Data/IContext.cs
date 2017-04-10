using Diploma.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Diploma.Data
{
    public interface IContext : IDisposable
    {
        DbSet<User> Users { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<CharacteristicsGroup> CharacteristicsGroups { get; set; }

        DbSet<Characteristic> Characteristics { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<Ban> Bans { get; set; }

        DbSet<OAuthState> OAuthStates { get; set; }

        DbSet<Token> Tokens { get; set; }

        Task<int> SaveChangesAsync();

        int SaveChanges();

        void Delete<TEntity>(TEntity entity, User deleteBy = null) where TEntity : class, IDeletable;

        void Edit<TEntity>(TEntity entity, User editBy = null) where TEntity : class;

        void Create<TEntity, TId>(TEntity entity, User createBy = null) where TEntity : class, IBaseEntity<TId>;
    }
}
