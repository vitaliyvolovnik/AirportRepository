using AutoMapper;
using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        public async Task<EmployeeDto?> GetEmployeeByUserAsync(int userId)
        {
            return _mapper.Map<EmployeeDto>(await _employeeRepository
                .FindFirstAsync(emp => emp.UserId == userId));
        }
    }
}
