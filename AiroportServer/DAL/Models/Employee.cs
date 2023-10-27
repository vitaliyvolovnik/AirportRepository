namespace DAL.Models
{
    public class Employee
    {

        public int Id { get; set; }

        public decimal Salury { get; set; }

        public string? Post { get; set; }

        public User? User { get; set; }
        public int UserId { get; set; }


    }
}