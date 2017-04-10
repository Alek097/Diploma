using Diploma.Core.ConfigureModels;
using Diploma.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;

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

        public DbSet<Token> Tokens { get; set; }

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
            optionsBuilder.UseSqlServer(this.app == null ? @"data source=(LocalDb)\v11.0;Initial Catalog=Diploma;Integrated Security=True;" : this.app.ConnectionString);
        }

        public void Delete<TEntity>(TEntity entity, User deleteBy = null)
            where TEntity : class, IDeletable
        {
            entity.IsDeleted = true;

            this.Edit(entity, deleteBy);
        }

        public void Edit<TEntity>(TEntity entity, User editBy = null)
            where TEntity : class
        {
            if (entity is IAuditable)
            {
                IAuditable auditableEntity = entity as IAuditable;

                if (editBy != null)
                {
                    auditableEntity.ModifyBy = editBy.Id;
                }

                auditableEntity.LastModifyDate = DateTime.Now;
            }

            this.Entry(entity).State = EntityState.Modified;
        }

        public void Create<TEntity, TId>(TEntity entity, User createBy = null)
            where TEntity : class, IBaseEntity<TId>
        {
            if (entity is IAuditable)
            {
                IAuditable auditableEntity = entity as IAuditable;

                if (createBy != null)
                {
                    auditableEntity.CreateBy = createBy.Id;
                    auditableEntity.ModifyBy = createBy.Id;
                }

                auditableEntity.CreateDate = DateTime.Now;
                auditableEntity.LastModifyDate = DateTime.Now;
            }

            this.Entry(entity).State = EntityState.Added;
        }
    }
}
