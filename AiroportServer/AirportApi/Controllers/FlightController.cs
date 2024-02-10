using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlaightService _flightService;
        private readonly ITerminalService _terminalService;

        public FlightController(IFlaightService flightService,
            ITerminalService terminal)
        {
            this._flightService = flightService;
            this._terminalService = terminal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _flightService.GetAllAsync());
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetPaged([FromRoute] int page, [FromRoute] int pageSize)
        {
            return Ok(await _flightService.GetPaged(page, pageSize));
        }

        [HttpGet("{page}/{pageSize}/{text}")]
        public async Task<IActionResult> GetPaged([FromRoute] int page,
            [FromRoute] int pageSize,
            [FromRoute] string text)
        {
            return Ok(await _flightService.GetPaged(page, pageSize, text));
        }

        [HttpGet("{page}/{pageSize}/{text}/{minPrice}/{maxPrice}")]
        public async Task<IActionResult> GetPaged([FromRoute] int page,
            [FromRoute] int pageSize,
            [FromRoute] string text,
            [FromRoute] decimal minPrice,
            [FromRoute] decimal maxPrice)
        {
            return Ok(await _flightService.GetPaged(page, pageSize, text, minPrice, maxPrice));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] FlightDto flightDto)
        {
            var flight = await _flightService.CreateAsync(flightDto);
            if (flight is null)
                return BadRequest();
            return Ok(flight);
        }

        [HttpPatch("{flightId}/{status}")]
        public async Task<IActionResult> ChangeStatusAsync([FromRoute] int flightId, [FromRoute] string status)
        {
            var flight = await _flightService.ChangeFlightStatusAsync(flightId, status);
            if (flight is null)
                return NotFound();

            return Ok(flight);
        }

        [HttpGet("terminals")]
        public async Task<IActionResult> GetAllTerminalsAsync()
        {
            return Ok(await _terminalService.GetAllAsync());
        }

        [HttpPost("terminals")]
        public async Task<IActionResult> CreateTerminalAsync([FromBody] TerminalDto terminal)
        {
            var created = await _terminalService.CreateAsync(terminal);
            if (created is null)
                return BadRequest();

            return Ok(created);
        }




    }
}
