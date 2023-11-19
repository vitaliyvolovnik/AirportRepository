using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public List<BookingDto> Bookings { get; set; }


        public UserDto? User { get; set; }
        public int UserId { get; set; }

        
    }
}
