using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IoTHomeAssistant.Domain.Entities;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IRepository<TEntity, in TType>
        where TEntity : class, IEntity<TType> 
        where TType : struct, IEquatable<TType>
    {
        TEntity Get(TType id);
        Task<TEntity> GetAsync(TType id);
        
        IEnumerable<TEntity> Get();
        IAsyncEnumerable<TEntity> GetAsync();
        
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        IAsyncEnumerable<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        
        void Delete(TType id);
        Task DeleteAsync(TType id);
        
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        
        void Commit();
        Task CommitAsync();
    }
}