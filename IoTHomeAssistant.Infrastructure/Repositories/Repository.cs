using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TType> : IRepository<TEntity, TType> 
        where TEntity : class, IEntity<TType> 
        where TType : struct, IEquatable<TType>
    {
        protected readonly IoTDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository(IoTDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        
        public TEntity Get(TType id) => _dbSet.FirstOrDefault(x => id.Equals(x.Id));
        public IEnumerable<TEntity> Get() => _dbSet.AsEnumerable();
        public IAsyncEnumerable<TEntity> GetAsync() => _dbSet.AsAsyncEnumerable();
        
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
            => _dbSet.Where(predicate).AsEnumerable();
        
        public async Task<TEntity> GetAsync(TType id)
            => await _dbSet.FirstOrDefaultAsync(x => id.Equals(x.Id));

        public IAsyncEnumerable<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => _dbSet.Where(predicate).AsAsyncEnumerable();
        
        public void Add(TEntity entity) => _dbSet.Add(entity);
        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
        
        public void Update(TEntity entity) => UpdateAsync(entity).RunSynchronously();
        
        public void Delete(TType id) => DeleteAsync(id).RunSynchronously();
        public void Delete(TEntity entity) => DeleteAsync(entity).RunSynchronously();
        
        public void Commit() => _dbContext.SaveChanges();
        public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
        
        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbSet.Attach(entity);
            _dbContext.Update(entity);

            await Task.CompletedTask;
        }
        
        public async Task DeleteAsync(TType id)
        {
            var dbItem = await _dbSet.FirstOrDefaultAsync(x => id.Equals(x.Id));
            if (dbItem != null)
            {
                _dbSet.Remove(dbItem);
            }

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            var dbItem = await _dbSet.FindAsync(entity);
            if (dbItem != null)
            {
                _dbSet.Remove(dbItem);
            }

            await Task.CompletedTask;
        }
    }
}