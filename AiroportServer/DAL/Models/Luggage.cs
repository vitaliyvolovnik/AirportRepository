namespace DAL.Models
{
    public class Luggage
    {
        public int Id { get; set; }

        public Booking? Booking { get; set; }
        public int BookingId { get; set; }

        public double Wiight { get; set; }


    }
}