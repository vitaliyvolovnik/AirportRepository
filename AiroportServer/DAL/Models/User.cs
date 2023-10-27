using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {

        public int Id { get; set; }

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }


        public string Role { get; set; } = "CUSTOMER";



    }
}
