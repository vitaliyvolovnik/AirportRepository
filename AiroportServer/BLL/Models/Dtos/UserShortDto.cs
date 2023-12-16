using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class UserShortDto
    {
        public int Id { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public bool IsEmailConfirmed { get; set; } = false;

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
