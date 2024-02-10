using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class FlightRepository : BaseRepository<Flight>
    {
        public FlightRepository(AirportContext airportContext) : base(airportContext)
        {
        }

        public override async Task<Flight?> FindFirstAsync(Expression<Func<Flight, bool>> predicate)
        {
            return await Entities
                .Include(x=>x.Crew)
                .ThenInclude(x=> x.User)
                .Include(x=>x.Plane)
                .Include(x=>x.Bookings)
                .FirstAsync(predicate)
                .ConfigureAwait(false);
        }
        public override async Task<IEnumerable<Flight>> FIndByConditionAsync(Expression<Func<Flight, bool>> predicate)
        {
            return await Entities
                .Include(x => x.Crew)
                .ThenInclude(x => x.User)
                .Include(x => x.Plane)
                .Include(x => x.Bookings)
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public override async Task<Flight?> UpdateAsync(Flight entity)
        {
            try
            {
                Entities.Attach(entity);
                var entry = _airportContext.Entry(entity);
                entry.Property(x => x.DepartureAddress).IsModified = !string.IsNullOrWhiteSpace(entity.DepartureAddress);
                entry.Property(x => x.ArrivalAddress).IsModified = !string.IsNullOrWhiteSpace(entity.ArrivalAddress);
                entry.Property(x => x.Status).IsModified = !string.IsNullOrWhiteSpace(entity.Status); 
                await _airportContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch { return null; }
        }

        public override async Task<IEnumerable<Flight>> GetAllAsync()
        {
            return await Entities
                .Include(x => x.Crew)
                .Include(x => x.Plane)
                .Include(x => x.Bookings)
                .ToListAsync()
                .ConfigureAwait(false);
        }


        public override async Task<PagedResult<Flight>> GetPagedAsync(int page, int pageSize)
        {
            var totalItems = await Entities.CountAsync().ConfigureAwait(false);
            var items = await Entities
                .Skip((page) * pageSize)
                .Take(pageSize)
                .Include(x => x.Crew)
                .ThenInclude(x => x.User)
                .Include(x => x.Plane)
                .Include(x => x.Bookings)
                .ToListAsync()
                .ConfigureAwait(false);

            return new PagedResult<Flight>()
            {
                TotalItems = totalItems,
                Result = items,
                PageIndex = page,
                PageSize = pageSize
            };
        }
    }
}
