using AutoMapper;
using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PlaneService : IPlaneService
    {
        private readonly IPlaneRepository _planeRepository;
        private readonly IMapper _mapper;

        public PlaneService(IPlaneRepository planeRepository, IMapper mapper)
        {
            _planeRepository = planeRepository;
            _mapper = mapper;
        }

        public async Task<PlaneDto?> ChangePlaneState(int planeId, PlaneState state)
        {
            var plane = await _planeRepository.FindFirstAsync(plane => plane.Id == planeId);
            if (plane is null)
                return null;
            plane.State = state;
            var updated = await _planeRepository.UpdateAsync(plane);
            return _mapper.Map<PlaneDto>(updated);
        }

        public async Task<PlaneDto?> CreateAsync(PlaneDto? plane)
        {
            var entity = _mapper.Map<Plane>(plane);
            return _mapper.Map<PlaneDto>(await _planeRepository.CreateAsync(entity));
        }

        public async Task<IEnumerable<PlaneDto>> GetAllAsync()
        {
            return (await _planeRepository.GetAllAsync()).Select(_mapper.Map<PlaneDto>);
        }

        public async Task<PlaneDto?> GetByIdAsync(int id)
        {
            return _mapper.Map<PlaneDto>(await _planeRepository.FindFirstAsync(plane => plane.Id == id));
        }

        public async Task<PagedResult<PlaneDto>> GetPaged(int page, int pageSize)
        {
            PagedResult<Plane> paged = await _planeRepository.GetPagedAsync(page, pageSize);

            return new PagedResult<PlaneDto>()
            {
                PageIndex = paged.PageIndex,
                PageSize = paged.PageSize,
                Result = paged.Result.Select(_mapper.Map<PlaneDto>).ToList(),
                TotalItems = paged.TotalItems
            };
        }

        public async Task<PlaneDto?> UpdatePalne(int planeId, PlaneDto plane)
        {
            var entity = _mapper.Map<Plane>(plane);
            entity.Id = planeId;
            return _mapper.Map<PlaneDto>(await _planeRepository.UpdateAsync(entity));
        }
    }
}
