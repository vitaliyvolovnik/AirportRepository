using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }

        public UserShortDto User { get; set; }
        public int UserId { get; set; }

        public FlightShortDto Flight { get; set; }
        public int FlightsId { get; set; }

        public List<LuggageDto> Luggage { get; set; }

        public DateTime BookingTime { get; set; }

        
    }
}
