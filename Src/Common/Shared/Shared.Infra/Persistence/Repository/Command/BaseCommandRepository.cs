using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interface.Repository.Command;

namespace Shared.Infra.Persistence.Repository.Command
{
    public class BaseCommandRepository<TEntity, TContext>
    : IBaseCommandRepository<TEntity>
    where TEntity : class
    where TContext : DbContext

    {
        protected readonly TContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseCommandRepository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        #region CRUD
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync<TKey>(TKey id) where TKey : notnull
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await Task.FromResult(true);
        }

        public async Task<Dictionary<TKey, bool>> DeleteRangeAsync<TKey>(List<TKey> ids) where TKey : notnull
        {
            var result = new Dictionary<TKey, bool>();
            foreach (var id in ids)
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    result[id] = true;
                }
                else
                {
                    result[id] = false;
                }
            }
            return result;
        }
        #endregion
        public void Dispose()
        {
         
        }

        

    }
}