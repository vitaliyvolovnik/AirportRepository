using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class FlightShortDto
    {
        public int Id { get; set; }

        public string DepartureAddress { get; set; }
        public string ArrivalAddress { get; set; }

        public string Status { get; set; }
        public decimal TicketCost { get; set; }

        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }

        public TerminalDto? Terminal { get; set; }
        public int TerminalId { get; set; }

        public PlaneDto? Plane { get; set; }
        public int PlaneId { get; set; }

        
    }
}
