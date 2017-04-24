using Diploma.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        DbSet<Role> Roles { get; set; }

        DbSet<Address> Addresses { get; set; }

        Task<int> SaveChangesAsync();

        int SaveChanges();

        void Delete<TEntity>(TEntity entity, Guid? deleteBy = null) where TEntity : class;

        void Modify<TEntity>(TEntity entity, Guid? modifyBy = null) where TEntity : class;

        void Create<TEntity, TId>(TEntity entity, Guid? createBy = null) where TEntity : class, IBaseEntity<TId>;

        Task DeleteAsync<TEntity>(TEntity entity, Guid? deleteBy = null) where TEntity : class;

        Task ModifyAsync<TEntity>(TEntity entity, Guid? modifyBy = null) where TEntity : class;

        Task CreateAsync<TEntity, TId>(TEntity entity, Guid? createBy = null) where TEntity : class, IBaseEntity<TId>;
    }
}
