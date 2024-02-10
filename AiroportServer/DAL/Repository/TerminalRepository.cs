using DAL.Context;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class TerminalRepository : BaseRepository<Terminal>
    {
        public TerminalRepository(AirportContext airportContext) : base(airportContext)
        {
        }
    }
}
