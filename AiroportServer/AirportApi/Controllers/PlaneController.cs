using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace AirportApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles ="EMPLOYEE")]
public class PlaneController : ControllerBase
{
    private IPlaneService _planeService;

    public PlaneController(IPlaneService planeService)
    {
        _planeService = planeService;

    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _planeService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var plane = await _planeService.GetByIdAsync(id);
        if (plane is null)
            return NotFound();
        return Ok(plane);
    }

    [HttpGet("{page}/{pageSize}")]
    public async Task<IActionResult> GetPaged([FromRoute] int page, [FromRoute] int pageSize)
    {
        return Ok(await _planeService.GetPaged(page, pageSize));
    }

    [HttpPost()]
    public async Task<IActionResult> Create([FromBody] PlaneDto plane)
    {
        var created = await _planeService.CreateAsync(plane);
        if (created is null)
            return BadRequest();
        return Ok(created);
    }

    [HttpPut("id")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PlaneDto plane)
    {
        var updated = await _planeService.UpdatePalne(id, plane);
        if (updated is null)
            return NotFound();
        return Ok(updated);
    }

}
