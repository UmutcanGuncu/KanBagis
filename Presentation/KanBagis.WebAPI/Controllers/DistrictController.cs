using KanBagis.Application.Mediator.Commands.District;
using KanBagis.Application.Mediator.Results.District;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanBagis.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,User")]
public class DistrictController(IMediator _mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> AddDistricts(CreateDistrictCommandRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddDistrictsExcel(IFormFile file)
    {
        var result = await _mediator.Send(new CreateDistrictExcelCommandRequest(file));
        if (result.Success)
         return Ok(result);
        return BadRequest(result);
    }
}