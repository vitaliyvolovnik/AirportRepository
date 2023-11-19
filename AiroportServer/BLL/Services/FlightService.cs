using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FlightService : IFlaightService
    {
        public Task<FlightDto?> CancelFlightAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FlightDto?> CreateAsync(FlightDto flaightDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFlightAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FlightShortDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
