using BLL.Models.AuthModels;
using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using DAL.Repository;
using DAL.Repository.Interfaces;
using BLL.Extentions;
using System.Security.Cryptography;
using System.Text;
using DAL.Models;

namespace BLL.Services
{
    public class AuthService:IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserDto?> ChangePasswordAsync(string email, string oldPass, string newPass)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
           return await _userRepository.IsEmailExistAsync(email);
        }

        public async Task<UserDto?> LoginAsync(Credentials credentials)
        {
            var passwordHash = HashPassword(credentials.Password);
            return (await _userRepository.FindFirstAsync(user =>
            user.Email == credentials.Email &&
            user.PasswordHash == passwordHash))?.ToDto();
        }

        private string HashPassword(string password)
        {
            var data = Encoding.ASCII.GetBytes(password);
            var md5 = MD5.Create();
            var hashData = md5.ComputeHash(data);
            var hashedPassword = BitConverter.ToString(hashData);
            return hashedPassword;
        }

        public async Task<UserDto?> RegisterAsync(RegisterModel registerModel)
        {
            if(!await _userRepository.IsEmailExistAsync(registerModel.Email))
            {
                var user = new User()
                {
                    Email = registerModel.Email,
                    Firstname = registerModel.Firstname,
                    Lastname = registerModel.Lastname,
                    PasswordHash = HashPassword(registerModel.Password),
                    Role = "Customer"
                };
                return (await _userRepository.CreateAsync(user))?.ToDto();

            }
            return null;
        }
    }
}
