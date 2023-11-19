using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserToken
    {
        public string Token { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public DateTime CreatedTime {get;set;}


        public TokenType Type { get; set; }
    }
}
