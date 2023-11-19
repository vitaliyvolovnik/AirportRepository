using DAL.Models;
using DAL.Models.Enums;

namespace DAL.Repository.Interfaces
{
    public interface IUserTokenRepository:IRepository<UserToken>
    {

        Task<UserToken?> UseAsync(string token, TokenType type);
        Task DeleteExpiresTokenAsync();
        
    }
}