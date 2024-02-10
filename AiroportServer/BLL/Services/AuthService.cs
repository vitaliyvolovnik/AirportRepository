using BLL.Models.AuthModels;
using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using DAL.Repository;
using DAL.Repository.Interfaces;
using BLL.Extentions;
using System.Security.Cryptography;
using System.Text;
using DAL.Models;
using DAL.Models.Enums;
using AutoMapper;

namespace BLL.Services
{
    public class AuthService:IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, 
            IUserTokenRepository userTokenRepository, 
            EmailService emailService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
            _emailService = emailService;
            _mapper = mapper;
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
                    Role = "CUSROMER",
                };
                var created = await _userRepository.CreateAsync(user);
                if(created is not null)
                {
                    var token = this.GenerateToken(user, TokenType.CONFIRM_EMAIL_TOKEN);
                    await _userTokenRepository.CreateAsync(token);

                    _emailService.SendEmailConfirmation(token.Token, user.Email);

                    return _mapper.Map<UserDto>(created);
                }

            }
            return null;
        }

        private UserToken GenerateToken(User user,TokenType type)
        {
            var token = Guid.NewGuid().ToString();
            token = token.Replace("_", "");
            return new UserToken()
            {
                Token = token,
                Type = type,
                User = user,
                CreatedTime = DateTime.Now,
            };
        }

        public async Task<UserDto?> ConfirmTokenAsync(string token)
        {
            
            var userToken = await _userTokenRepository.UseAsync(token, TokenType.CONFIRM_EMAIL_TOKEN);

            if(userToken is not null) 
            {
               userToken.User.IsEmailConfirmed = true;
                return _mapper.Map<UserDto>(await _userRepository.UpdateAsync(userToken.User));
            }
            return null;
        }
    }
}
