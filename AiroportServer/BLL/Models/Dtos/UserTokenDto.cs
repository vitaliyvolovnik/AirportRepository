using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class UserTokenDto
    {
        public string Token { get; set; }

        public UserDto User { get; set; }
        public int UserId { get; set; }

        public DateTime CreatedTime { get; set; }

        public JsonTokenType Type { get; set; }
    }
}
