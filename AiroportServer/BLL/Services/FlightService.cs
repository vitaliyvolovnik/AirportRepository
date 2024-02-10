using AutoMapper;
using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repository;
using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FlightService : IFlaightService
    {
        private readonly FlightRepository _flightRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public FlightService(FlightRepository flightRepository, IMapper mapper,IEmployeeRepository employeeRepository)
        {
            _flightRepository = flightRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        public async Task<FlightDto?> ChangeFlightStatusAsync(int id, string status)
        {
            var flight = await _flightRepository.FindFirstAsync(x => x.Id == id);
            if (flight is null)
                return null;

            flight.Status = status;
            var updated = await _flightRepository.UpdateAsync(flight);

            return _mapper.Map<FlightDto?>(updated);
        }

        public async Task<FlightDto?> CreateAsync(FlightDto flaightDto)
        {
            var flight = _mapper.Map<Flight>(flaightDto);
            flight.Status = "EXPECTED";
            
            flight.PlaneId = flight.Plane.Id;
            flight.Plane = null;
            var crewIdes = flight.Crew.Select(x=>x.Id).ToList();
            var employees = await _employeeRepository
                .FIndByConditionAsync(x => crewIdes.Any(c => c == x.Id));
            flight.Crew = employees.ToList();

            flight.TerminalId = flight.Terminal.Id;
            flight.Terminal = null;
            return _mapper.Map<FlightDto?>(await _flightRepository.CreateAsync(flight));
        }

        public async Task DeleteFlightAsync(int id)
        {
            await _flightRepository.DeleteAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<FlightShortDto>> GetAllAsync()
        {
            return (await _flightRepository.GetAllAsync()).Select(_mapper.Map<FlightShortDto>);
        }


        public async Task<PagedResult<FlightDto>> GetPaged(int page, int pageSize, string content, decimal minPrice, decimal maxPrice)
        {
            var tempResult = new PagedResult<Flight>();


            if (!string.IsNullOrEmpty(content))
            {
                var words = content.Split(' ');

                tempResult = await _flightRepository.GetPagedAsync(
                    flight => (words.Any(word => flight.ArrivalAddress.Contains(word) ||
                        flight.DepartureAddress.Contains(word)))&& 
                        (flight.TicketCost >= minPrice && flight.TicketCost <= maxPrice),
                    page,
                    pageSize);
            }
            else
            {
                tempResult = await _flightRepository.GetPagedAsync(
                    flight =>  flight.TicketCost>= minPrice && flight.TicketCost<= maxPrice,
                    page,
                    pageSize);
            }

            return new PagedResult<FlightDto>()
            {
                PageIndex = tempResult.PageIndex,
                PageSize = tempResult.PageSize,
                TotalItems = tempResult.TotalItems,
                Result = tempResult.Result.Select(_mapper.Map<FlightDto>).ToList()
            };
        }

        public async Task<PagedResult<FlightDto>> GetPaged(int page, int pageSize)
        {
            var tempResult = await _flightRepository.GetPagedAsync(page, pageSize);
            return new PagedResult<FlightDto>()
            {
                PageIndex = tempResult.PageIndex,
                PageSize = tempResult.PageSize,
                TotalItems = tempResult.TotalItems,
                Result = tempResult.Result.Select(_mapper.Map<FlightDto>).ToList()
            };
        }

        public async Task<PagedResult<FlightDto>> GetPaged(int page, int pageSize, string content)
        {
            var words = content.Split(' ');

            var tempResult = await _flightRepository.GetPagedAsync(
                flight => words.Any(word => flight.ArrivalAddress.Contains(word) ||
                    flight.DepartureAddress.Contains(word)),
                page,
                pageSize);

            return new PagedResult<FlightDto>()
            {
                PageIndex = tempResult.PageIndex,
                PageSize = tempResult.PageSize,
                TotalItems = tempResult.TotalItems,
                Result = tempResult.Result.Select(_mapper.Map<FlightDto>).ToList()
            };
        }
    }
}
