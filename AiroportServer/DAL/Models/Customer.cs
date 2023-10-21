using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Customer
    {

        public int Id { get; set; }

        public List<Booking> Bookings { get; set; }


        public User User { get; set; }
        public int UserId { get; set; }


    }
}
