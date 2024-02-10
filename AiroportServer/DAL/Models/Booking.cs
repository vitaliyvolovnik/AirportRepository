namespace DAL.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Flight Flight { get; set; }
        public int FlightsId { get; set;}

        public List<Luggage> Luggage { get; set; } 
        
        public DateTime BookingTime { get; set; }
    }
}