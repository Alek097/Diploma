using Diploma.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Repositories
{
    public interface IRepository<T, TId> : IDisposable
        where T : class, IBaseEntity<TId>
    {
        Task<IList<T>> GetAsync();
        Task<T> GetAsync(TId id);
        Task AddAsync(T entity, Guid? createBy= null);
        Task ModifyAsync(T entity, Guid? modifyBy = null);
        Task DeleteAsync(T entity, Guid? deleteBy = null);

        IList<T> Get();
        T Get(TId id);
        void Add(T entity, Guid? createBy = null);
        void Modify(T entity, Guid? modifyBy = null);
        void Delete(T entity, Guid? deleteBy = null);

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
