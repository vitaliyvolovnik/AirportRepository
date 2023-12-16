using BLL.Models.Dtos;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IAdministrationService
    {
        Task<EmployeeDto?> UpdateAsync(EmployeeDto employee,int employeeId);
        Task<EmployeeDto?> GetAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetByPostAsync(string post);
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto?> PromoteUserAsync(int userId);
        Task<EmployeeDto?> DismissEmployeeAsync(int EmployeeId);
        Task<PagedResult<EmployeeDto>> GetPagedResultAsync(int page,int pageSize);
    }
}
