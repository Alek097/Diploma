using Diploma.Data;
using Diploma.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma.Repositories
{
    public abstract class Repository<T, TId> : IRepository<T, TId>
        where T : class, IBaseEntity<TId>
    {
        protected IContext context;

        public Repository(IContext context)
        {
            this.context = context;
        }

        public virtual async Task AddAsync(T entity, Guid? createBy = null)
        {
            await this.context.CreateAsync<T, TId>(entity, createBy);
        }

        public virtual async Task DeleteAsync(T entity, Guid? deleteBy = null)
        {
            await this.context.DeleteAsync(entity, deleteBy);
        }

        public virtual async Task ModifyAsync(T entity, Guid? modifyBy = null)
        {
            await this.context.ModifyAsync(entity, modifyBy);
        }

        public abstract IList<T> Get();

        public virtual T Get(TId id)
        {
            return this.Get()
                .FirstOrDefault(entity => entity.Id.Equals(id));
        }

        public virtual void Add(T entity, Guid? createBy = default(Guid?))
        {
            this.context.Create<T, TId>(entity, createBy);
        }

        public virtual void Modify(T entity, Guid? modifyBy = default(Guid?))
        {
            this.context.Modify(entity, modifyBy);
        }

        public virtual void Delete(T entity, Guid? deleteBy = default(Guid?))
        {
            this.context.Delete(entity, deleteBy);
        }

        public virtual async Task<IList<T>> GetAsync()
        {
            return await Task.Run(() => this.Get());
        }

        public virtual async Task<T> GetAsync(TId id)
        {
            return await Task.Run(() => this.Get(id));
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }
    }
}
