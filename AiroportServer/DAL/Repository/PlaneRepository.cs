using DAL.Context;
using DAL.Models;
using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class PlaneRepository : BaseRepository<Plane>, IPlaneRepository
    {
        public PlaneRepository(AirportContext airportContext) : base(airportContext)
        {
        }
    }
}
