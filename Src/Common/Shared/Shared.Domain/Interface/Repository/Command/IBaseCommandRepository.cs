using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Interface.Repository.Command
{
    public interface IBaseCommandRepository<TEntity> : IDisposable
    where TEntity : class
    {

        #region CRUD
        Task AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync<TKey>(TKey id) where TKey : notnull;
        Task<bool> DeleteAsync(TEntity entity);
        Task<Dictionary<TKey, bool>> DeleteRangeAsync<TKey>(List<TKey> ids) where TKey : notnull;
        #endregion


    }
}
