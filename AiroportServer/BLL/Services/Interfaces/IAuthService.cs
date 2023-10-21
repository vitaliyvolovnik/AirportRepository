using BLL.Models.AuthModels;
using BLL.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IAuthService
    {

        Task<UserDto?> LoginAsync(Credentials credentials);

        Task<UserDto?> RegisterAsync(RegisterModel registerModel);

        Task<bool> IsEmailExistAsync(string email);

        Task<UserDto?> ChangePasswordAsync(string email,string oldPass,string newPass);
    }
}
