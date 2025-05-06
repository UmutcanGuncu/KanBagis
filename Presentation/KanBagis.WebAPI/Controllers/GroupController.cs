using KanBagis.Application.Mediator.Commands.Group;
using KanBagis.Application.Mediator.Queries.Group;
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
    public async Task<IActionResult> CreateGroup(CreateGroupCommandRequest createGroupCommandRequest)
    {
        var result = await _mediator.Send(createGroupCommandRequest);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddUserToGroup(AddUserToGroupCommandRequest addUserToGroupCommandRequest)
    {
        var result = await _mediator.Send(addUserToGroupCommandRequest);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddBloodDonationToGroup(
        AddBloodDonationToGroupCommandRequest addBloodDonationToGroupCommandRequest)
    {
        var result = await _mediator.Send(addBloodDonationToGroupCommandRequest);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GetGroupBySupervisorId(GetGroupBySupervisorIdQuery request)
    {
        var results = await _mediator.Send(request);
        return Ok(results);
    }
}