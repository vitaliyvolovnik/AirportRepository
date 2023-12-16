using BLL.Models.Dtos;
using DAL.Models;
using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IPlaneService
    {

        Task<PlaneDto?> CreateAsync(PlaneDto? plane);

        Task<IEnumerable<PlaneDto>> GetAllAsync();
        Task<PlaneDto?> GetByIdAsync(int id);

        Task<PlaneDto?> ChangePlaneState(int planeId, PlaneState state);
        Task<PlaneDto?> UpdatePalne(int planeId, PlaneDto plane);

        Task<PagedResult<PlaneDto>> GetPaged(int page,int pageSize);
    }
}
