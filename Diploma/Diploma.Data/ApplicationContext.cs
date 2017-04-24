using Diploma.Core.ConfigureModels;
using Diploma.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Diploma.Data
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IContext
    {
        private readonly App app;

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CharacteristicsGroup> CharacteristicsGroups { get; set; }

        public DbSet<Characteristic> Characteristics { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Ban> Bans { get; set; }

        public DbSet<OAuthState> OAuthStates { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public new DbSet<Role> Roles { get; set; }

        public ApplicationContext(IOptions<App> app)
        {
            this.app = app.Value;

            this.Database.Migrate();
        }

        public ApplicationContext() { }

        public async Task<int> SaveChangesAsync()
        {
            return await Task.Run<int>(() => this.SaveChanges());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.app == null ? @"data source=.\;Initial Catalog=Diploma;Integrated Security=True;" : this.app.ConnectionString);
        }

        public void Delete<TEntity>(TEntity entity, Guid? deleteBy = null)
            where TEntity : class
        {
            if (entity is IDeletable)
            {
                (entity as IDeletable).IsDeleted = true;

                this.Modify(entity, deleteBy);
            }
            else
            {
                this.Entry<TEntity>(entity).State = EntityState.Deleted;
            }
        }

        public void Modify<TEntity>(TEntity entity, Guid? modifyBy = null)
            where TEntity : class
        {
            if (entity is IAuditable)
            {
                IAuditable auditableEntity = entity as IAuditable;

                if (modifyBy != null)
                {
                    auditableEntity.ModifyBy = modifyBy;
                }

                auditableEntity.LastModifyDate = DateTime.Now;
            }

            this.Entry(entity).State = EntityState.Modified;
        }

        public void Create<TEntity, TId>(TEntity entity, Guid? createBy = null)
            where TEntity : class, IBaseEntity<TId>
        {
            if (entity is IAuditable)
            {
                IAuditable auditableEntity = entity as IAuditable;

                if (createBy != null)
                {
                    auditableEntity.CreateBy = createBy;
                    auditableEntity.ModifyBy = createBy;
                }

                auditableEntity.CreateDate = DateTime.Now;
                auditableEntity.LastModifyDate = DateTime.Now;
            }

            this.Entry(entity).State = EntityState.Added;
        }

        public async Task DeleteAsync<TEntity>(TEntity entity, Guid? deleteBy = null) where TEntity : class
        {
            await Task.Run(() => this.Delete(entity, deleteBy));
        }

        public async Task ModifyAsync<TEntity>(TEntity entity, Guid? modifyBy = null) where TEntity : class
        {
            await Task.Run(() => this.Modify(entity, modifyBy));
        }

        public async Task CreateAsync<TEntity, TId>(TEntity entity, Guid? createBy = null) where TEntity : class, IBaseEntity<TId>
        {
            await Task.Run(() => this.Create<TEntity, TId>(entity, createBy));
        }
    }
}
