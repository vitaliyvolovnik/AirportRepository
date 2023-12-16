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
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AirportContext airportContext) : base(airportContext)
        {
        }

        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await Entities
                .Include(employee => employee.User)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public override async Task<Employee?> FindFirstAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await Entities
                .Include(employee => employee.User)
                .FirstOrDefaultAsync(predicate)
                .ConfigureAwait(false);
        }

        public override async Task<IEnumerable<Employee>> FIndByConditionAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await Entities
                .Include(employee => employee.User)
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public override async Task<Employee?> UpdateAsync(Employee entity)
        {
            try
            {
                Entities.Attach(entity);
                var entry = _airportContext.Entry(entity);
                entry.Property(x => x.Post).IsModified = !string.IsNullOrWhiteSpace(entity.Post);
                entry.Property(x => x.Salury).IsModified = entity.Salury > -1;
                await _airportContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch { return null; }
        }

        public override async Task<PagedResult<Employee>> GetPagedAsync(int page, int pageSize)
        {
            var totalItems = await Entities.CountAsync().ConfigureAwait(false);
            var items = await Entities
                .Skip((page) * pageSize)
                .Take(pageSize)
                .Include(employee => employee.User)
                .ToListAsync()
                .ConfigureAwait(false);

            return new PagedResult<Employee>()
            {
                TotalItems = totalItems,
                Result = items,
                PageIndex = page,
                PageSize = pageSize
            };
        }


        public override async Task<PagedResult<Employee>> GetPagedAsync(
            Expression<Func<Employee, bool>> predicate,
            int page,
            int pageSize,
            Expression<Func<Employee, object>> orderBy = null,
            bool isDescending = false)
        {


            IQueryable<Employee> query = Entities.Where(predicate);

            if (orderBy is not null)
            {
                if (isDescending)
                    query = query.OrderByDescending(orderBy);
                else
                    query = query.OrderBy(orderBy);
            }

            var orderedEntities = await query.Skip(page * pageSize)
                .Take(pageSize)
                .Include(employee => employee.User)
                .ToListAsync()
                .ConfigureAwait(false);


            var totalItems = await Entities.CountAsync().ConfigureAwait(false);

            return new PagedResult<Employee>
            {
                Result = orderedEntities,
                TotalItems = totalItems,
                PageIndex = page,
                PageSize = pageSize
            };
        }




    }
}
