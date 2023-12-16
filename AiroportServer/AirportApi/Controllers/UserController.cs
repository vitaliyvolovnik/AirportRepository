using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetPaged([FromRoute] int page,
            [FromRoute] int pageSize)
        {
            return Ok(await _userService.GetPagedResultAsync(page, pageSize));
        }


    }
}
