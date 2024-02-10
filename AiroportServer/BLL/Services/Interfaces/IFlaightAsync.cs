using BLL.Models.Dtos;
using DAL.Models;
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
        Task<FlightDto?> ChangeFlightStatusAsync(int id, string status);
        Task DeleteFlightAsync(int id);

        Task<PagedResult<FlightDto>> GetPaged(int page, int pageSize);
        Task<PagedResult<FlightDto>> GetPaged(int page, int pageSize, string text);
        Task<PagedResult<FlightDto>> GetPaged(int page, int pageSize,
             string text,
             decimal minPrice,
             decimal maxPrice);
    }
}
