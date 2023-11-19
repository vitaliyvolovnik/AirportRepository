using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Plane
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public int Capacity { get; set; }

        public PlaneState State { get; set; }



    }
}
