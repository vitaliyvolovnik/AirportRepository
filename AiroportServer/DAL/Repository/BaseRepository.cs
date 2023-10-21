using DAL.Context;
using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AirportContext _airportContext;

        public BaseRepository(AirportContext airportContext)
        {
            _airportContext = airportContext;
        }

        private DbSet<TEntity> entities;
        protected DbSet<TEntity> Entities => entities ??= _airportContext.Set<TEntity>();

        public virtual async Task<TEntity?> CreateAsync(TEntity entity)
        {
            try
            {
                var entry = await Entities.AddAsync(entity);
                await _airportContext.SaveChangesAsync().ConfigureAwait(false);

                return entry.Entity;
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var forDelete = Entities.Where(predicate);
            Entities.RemoveRange(forDelete);
            await _airportContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            try
            {
                Entities.Attach(entity);
                var entry = _airportContext.Entry(entity);
                entry.State = EntityState.Modified;
                await _airportContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch { return null; }
        }

        public virtual async Task<TEntity?> FindFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.FirstOrDefaultAsync(predicate).ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Entities.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> FIndByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        public async Task<PagedResult<TEntity>> GetPagedAsync(int page, int pageSize)
        {
            var totalItems = await Entities.CountAsync().ConfigureAwait(false);
            var items = await Entities
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
            return new PagedResult<TEntity>()
            {
                TotalItems = totalItems,
                Result = items,
                PageIndex = page,
                PageSize = pageSize
            };
        }

        public async Task<PagedResult<TEntity>> GetPagedAsync(
            Expression<Func<TEntity, bool>> predicate, 
            int page, 
            int pageSize, 
            Expression<Func<TEntity, object>> orderBy = null, 
            bool isDescending = false)
        {


            IQueryable<TEntity> query = Entities.Where(predicate);

            if (isDescending)
                query = query.OrderByDescending(orderBy);
            else
                query = query.OrderBy(orderBy);

            var orderedEntities = await query.Skip((page - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToListAsync()
                                       .ConfigureAwait(false);

            var totalItems = await Entities.CountAsync().ConfigureAwait(false);

            return new PagedResult<TEntity>
            {
                Result = orderedEntities,
                TotalItems = totalItems,
                PageIndex = page,
                PageSize = pageSize
            };
        }
    }
}
