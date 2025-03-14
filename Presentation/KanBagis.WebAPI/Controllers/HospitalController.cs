using KanBagis.Application.DTOs;
using KanBagis.Application.Mediator.Commands.Hospital;
using KanBagis.Application.Mediator.Queries.Hospital;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanBagis.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,User")]
public class HospitalController(IMediator _mediator) :ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetFilteredHospitals([FromQuery] GetFilteredHospitalQuery query)
    {
        var values = await _mediator.Send(query);   
        return Ok(values);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddHospital(IFormFile file)
    {
        if (file == null)
            return BadRequest();
        
        var result = await _mediator.Send(new CreateHospitalsCommandRequest(file));
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}