using KanBagis.Application.Mediator.Commands.Group;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanBagis.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin,User")]
public class GroupController(IMediator _mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> AddGroup(CreateGroupCommandRequest createGroupCommandRequest)
    {
        var result = await _mediator.Send(createGroupCommandRequest);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}