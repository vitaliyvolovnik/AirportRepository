using DAL.Models;
using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class PlaneDto
    {
        public int? Id { get; set; }

        public string Manufacturer { get; set; }

        public int MaxCargoWeigth { get; set; }

        public string Model { get; set; }

        public int Capacity { get; set; }
            
        public PlaneState? State { get; set; }
       
    }
}
