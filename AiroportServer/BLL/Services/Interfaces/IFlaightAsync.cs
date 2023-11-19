using BLL.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IFlaightService
    {

        Task<FlightDto?> CreateAsync(FlightDto flaightDto);

        Task<IEnumerable<FlightShortDto>> GetAllAsync();
        Task<FlightDto?> CancelFlightAsync(int id);
        Task DeleteFlightAsync(int id);
    }
}
