using AutoMapper;
using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto?> AddUserAsync(UserDto userDto)
        {
            User? user = _mapper.Map<User>(userDto);
            return _mapper.Map<UserDto>(await _userRepository.CreateAsync(user));
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _userRepository.DeleteAsync(user => user.Id == userId);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return _mapper.Map<IEnumerable<UserDto>>(await _userRepository.GetAllAsync());
        }

        public async Task<UserDto?> GetUserAsync(int userId)
        {
            return _mapper.Map<UserDto>(await _userRepository.FindFirstAsync(x => x.Id == userId));
        }

        public async Task<UserDto?> GetUserAsync(string email)
        {
            return _mapper.Map<UserDto>(await _userRepository.FindFirstAsync(x => x.Email == email));
        }

        public async Task<UserDto?> UpdateUserAsync(UserDto newUser, int oldUserId)
        {
            newUser.Id = oldUserId;
            return _mapper.Map<UserDto>(await _userRepository.UpdateAsync(_mapper.Map<User>(newUser)));
        }
    }
}
