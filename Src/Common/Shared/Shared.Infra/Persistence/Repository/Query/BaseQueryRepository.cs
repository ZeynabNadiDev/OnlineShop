using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interface.Repository.Query;
using Shared.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infra.Persistence.Repository.Query
{
    public class BaseQueryRepository<TEntity, TContext>
        : IBaseQueryRepository<TEntity>
        where TEntity : class
    where TContext : DbContext

    {
        protected readonly TContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseQueryRepository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        #region Get Methodes
        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
            => await _dbSet.AsNoTracking().Where(expression).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetByIdAsync<TKey>(TKey id) where TKey : notnull
           => await _dbSet.FindAsync(id);

        public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
           => await _dbSet.AsNoTracking().SingleOrDefaultAsync(expression);

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
           => await _dbSet.AsNoTracking().FirstOrDefaultAsync(expression);

        #endregion

        #region Get Dto Methods

        public async Task<IEnumerable<TDto>> GetDtoAsync<TDto>(
            Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TDto>> selectExpression)
        => await _dbSet.AsNoTracking()
                        .Where(expression)
                        .Select(selectExpression)
                        .ToListAsync();

        public async Task<IEnumerable<TDto>> GetAllDtoAsync<TDto>(
            Expression<Func<TEntity, TDto>> selectExpression)
         => await _dbSet.AsNoTracking().Select(selectExpression).ToListAsync();

        public async Task<TDto?> GetByIdDtoAsync<TKey, TDto>(
            TKey id, Expression<Func<TEntity, TDto>> selectExpression)
            where TKey : notnull
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return default;

            return await _dbSet
                .Where(e => e == entity)
                .Select(selectExpression)
                .FirstOrDefaultAsync();
        }

        public async Task<TDto?> SingleOrDefaultDtoAsync<TDto>(
            Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TDto>> selectExpression)
         => await _dbSet.AsNoTracking()
                         .Where(expression)
                         .Select(selectExpression)
                         .SingleOrDefaultAsync();

        public async Task<TDto?> FirstOrDefaultDtoAsync<TDto>(
            Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TDto>> selectExpression)
         => await _dbSet.AsNoTracking()
                         .Where(expression)
                         .Select(selectExpression)
                         .FirstOrDefaultAsync();


        #endregion

        #region Get Whith Pagination
        public async Task<Pagenation<TEntity>> GetWithPaginationAsync(
            Expression<Func<TEntity, bool>> expression,
            int page = 1, int pageSize = 10)
        {
            var query = _dbSet.AsNoTracking().Where(expression);
            var count = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Pagenation<TEntity>(items, count, page, pageSize);
        }

        public async Task<Pagenation<TEntity>> GetAllWithPaginationAsync(
            int page = 1, int pageSize = 10)
        {
            var query = _dbSet.AsNoTracking();
            var count = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Pagenation<TEntity>(items, count, page, pageSize);
        }

        #endregion

        #region Utility
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
          => await _dbSet.AnyAsync(expression);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
          => await _dbSet.CountAsync(expression);

        #endregion

        #region Queryable
        public IQueryable<TEntity> Query()
         => _dbSet.AsQueryable();

        #endregion
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
