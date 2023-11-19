using AutoMapper;
using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public AdministrationService(IUserRepository userRepository,IEmployeeRepository employeeRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto?> DismissEmployeeAsync(int employeeId)
        {
            var employee = await _employeeRepository.FindFirstAsync(emp => emp.Id == employeeId);


            if (employee is null)
                return null;

            employee.User.Role = "CUSTOMER";

            await _userRepository.UpdateAsync(employee.User);

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            return (await _employeeRepository.GetAllAsync()).Select(_mapper.Map<EmployeeDto>);
        }

        public async Task<EmployeeDto?> GetAsync(int id)
        {
            return _mapper.Map<EmployeeDto>(await _employeeRepository.FindFirstAsync(emp => emp.Id == id));
        }

        public async Task<EmployeeDto?> PromoteUserAsync(int userId)
        {
            var user = await _userRepository.FindFirstAsync(x => x.Id == userId);

            if(user is null) return null;

            Employee emp = new Employee();
            emp.User = user;

            return _mapper.Map<EmployeeDto>(await _employeeRepository.CreateAsync(emp));
        }

        public async Task<EmployeeDto?> UpdateAsync(EmployeeDto employee,int employeeId)
        {
            var emp = _mapper.Map<Employee>(employee);
            emp.Id = employeeId;
            return _mapper.Map<EmployeeDto?>(await _employeeRepository.UpdateAsync(emp));

        }
    }
}
