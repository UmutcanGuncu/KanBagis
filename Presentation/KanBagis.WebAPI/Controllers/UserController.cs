using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.UserInformation;
using KanBagis.Application.Mediator.Queries.UserInformation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanBagis.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,User")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetUserInformation(string userId)
    {
        var result = await mediator.Send(new GetUserInformationQuery(){UserId = userId});
        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateUserInformation(UpdateUserInformationCommandRequest updateUserInformationCommandRequest)
    {
        var result = await mediator.Send(updateUserInformationCommandRequest);
        if(result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}