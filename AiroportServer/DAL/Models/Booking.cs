namespace DAL.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public Flight Flights { get; set; }
        public int FlightsId { get; set;}

        public List<Luggage> Luggage { get; set; } 
        
        public DateTime BookingTime { get; set; }
    }
}