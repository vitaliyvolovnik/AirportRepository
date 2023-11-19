using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "MANAGER")]
    public class AdminController : ControllerBase
    {
        private readonly IAdministrationService _adminService;

        public AdminController(IAdministrationService administrationService)
        {
            _adminService = administrationService;
        }


        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _adminService.GetAllAsync());
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> Get([FromRoute] int employeeId)
        {
            var emp = await _adminService.GetAsync(employeeId);
            if (emp is null)
                return NotFound();

            return Ok(emp);
        }

        [HttpPatch("dismit/{employeeId}")]
        public async Task<IActionResult> DismitEmployee([FromRoute] int employeeId)
        {
            var emp = await _adminService.DismissEmployeeAsync(employeeId);
            if (emp is null) return NotFound();

            return Ok(emp);
        }

        [HttpPost("promote/{userId}")]
        public async Task<IActionResult> PromoteUser([FromRoute] int userId)
        {
            var user = await _adminService.PromoteUserAsync(userId);
            if(user is null) return NotFound(); 

            return Ok(user);
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int employeeId, [FromBody] EmployeeDto employee)
        {
            var updeted = await _adminService.UpdateAsync(employee, employeeId);
            if(updeted  is null) return NotFound();
            return Ok(updeted);
        }

    }
}
