using KanBagis.Application.Mediator.Commands.City;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanBagis.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,User")]
public class CityController(IMediator _mediator) : ControllerBase
{
    [HttpPost("[action]")]

    public async Task<IActionResult> AddCity(CreateCityCommandRequest createCityCommandRequest)
    {
        var result = await _mediator.Send(createCityCommandRequest);
        if(result.Success)
            return Ok(result.Message);
        return BadRequest(result.Message);
    }
}