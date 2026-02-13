using Shared.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Interface.Repository.Query
{
    public interface IBaseQueryRepository<TEntity> : IDisposable
    where TEntity : class
    {
        #region Get Methods 
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync<TKey>(TKey id) where TKey : notnull;
        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region Get Dto Methods 
        Task<IEnumerable<TDto>> GetDtoAsync<TDto>(
            Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TDto>> selectExpression);

        Task<IEnumerable<TDto>> GetAllDtoAsync<TDto>(
            Expression<Func<TEntity, TDto>> selectExpression);

        Task<TDto?> GetByIdDtoAsync<TKey, TDto>(
            TKey id, Expression<Func<TEntity, TDto>> selectExpression)
            where TKey : notnull;

        Task<TDto?> SingleOrDefaultDtoAsync<TDto>(
            Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TDto>> selectExpression);

        Task<TDto?> FirstOrDefaultDtoAsync<TDto>(
            Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TDto>> selectExpression);
        #endregion

        #region Get With Pagination
        Task<Pagenation<TEntity>> GetWithPaginationAsync(
            Expression<Func<TEntity, bool>> expression,
            int page = 1,
            int pageSize = 10);

        Task<Pagenation<TEntity>> GetAllWithPaginationAsync(
            int page = 1,
            int pageSize = 10);
        #endregion

        #region Utility
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region Queryable
        IQueryable<TEntity> Query();
        #endregion


    }
}
