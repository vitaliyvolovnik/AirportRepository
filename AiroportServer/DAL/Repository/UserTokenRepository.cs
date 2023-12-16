using DAL.Context;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserTokenRepository : BaseRepository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(AirportContext airportContext) : base(airportContext)
        {
        }


        public override async Task<UserToken?> FindFirstAsync(Expression<Func<UserToken, bool>> predicate)
        {
            try
            {
                return await Entities
                .Include(token => token.User)
                .FirstAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task DeleteExpiresTokenAsync()
        {
            var toDelete = Entities
                .Where(x => x.CreatedTime < DateTime.Now.AddMinutes(-30));
            Entities.RemoveRange(toDelete);

            await _airportContext.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task<UserToken?> UseAsync(string token, TokenType type)
        {
            var userToken = await FindFirstAsync(t => t.Token == token && t.Type == type);


            if (userToken is null || userToken.CreatedTime < DateTime.Now.AddMinutes(-30))
                return null;

            Entities.Remove(userToken);
            await _airportContext.SaveChangesAsync()
                .ConfigureAwait(false);

            return userToken;
        }
    }

}
