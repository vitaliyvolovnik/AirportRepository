using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class CustomerShortDto
    {
        public int Id { get; set; }


        public UserShortDto? User { get; set; }
        public int UserId { get; set; }
    }
}
