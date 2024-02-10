namespace DAL.Models
{
    public class Flight
    {

        public int Id { get; set; }

        public string DepartureAddress { get; set; }

        public string ArrivalAddress { get; set; }

        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }

        public string Status { get; set; }

        public decimal TicketCost { get; set; }

        public List<Booking> Bookings { get; set; }

        public List<Employee> Crew { get; set; } 

        public Terminal? Terminal { get; set; }
        public int TerminalId { get; set; }

        public Plane? Plane { get; set; }
        public int PlaneId { get; set; }
    }
}