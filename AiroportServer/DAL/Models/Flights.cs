namespace DAL.Models
{
    public class Flight
    {

        public int Id { get; set; }

        public string DepartureAddress { get; set; }
        public string ArrivalAddress { get; set; }

        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }


        public List<Booking> Bookings { get; set; }

        public List<Employee> Crew { get; set; } 

        public Gate? Gate { get; set; }
        public int GateId { get; set; }

        public Plane? Plane { get; set; }
        public int PlaneId { get; set; }
    }
}