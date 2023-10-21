using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {

        Task<TEntity?> CreateAsync(TEntity entity);

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> UpdateAsync(TEntity entity);

        Task<TEntity?> FindFirstAsync(Expression<Func<TEntity, bool>> predicate);
        
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FIndByConditionAsync(Expression<Func<TEntity, bool>> predicate);
        Task<PagedResult<TEntity>> GetPagedAsync(int page, int pageSize);
        Task<PagedResult<TEntity>> GetPagedAsync(
            Expression<Func<TEntity, bool>> predicate, 
            int page, 
            int pageSize, 
            Expression<Func<TEntity, object>> orderBy = null, 
            bool isDescending = false);


    }
}
