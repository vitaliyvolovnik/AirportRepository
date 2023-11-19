using BLL.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> AddUserAsync(UserDto userDto);
        Task DeleteUserAsync(int userId);
        Task<UserDto?> GetUserAsync(int userId);
        Task<UserDto?> GetUserAsync(string email);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> UpdateUserAsync(UserDto newUser, int oldUserId);
    }
}
