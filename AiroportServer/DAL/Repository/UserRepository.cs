using DAL.Context;
using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AirportContext airportContext) : base(airportContext)
        {
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await Entities.AnyAsync(user => user.Email == email).ConfigureAwait(false);
        }

        
    }
}
