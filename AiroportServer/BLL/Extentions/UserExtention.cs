using BLL.Models.Dtos;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extentions
{
    public static class UserExtention
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Firstname = user.Firstname, 
                Lastname = user.Lastname,
                Role = user.Role
            };
        }

    }
}
