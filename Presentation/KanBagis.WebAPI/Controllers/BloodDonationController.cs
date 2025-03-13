using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.BloodDonation;
using KanBagis.Application.Mediator.Queries.BloodDonation;
using KanBagis.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanBagis.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,User")]
public class BloodDonationController(IMediator _mediator): ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> AddBloodDonation(CreateBloodDonationCommandRequest createBloodDonationCommandRequest)
    {
        var result = await _mediator.Send(createBloodDonationCommandRequest);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllBloodDonations()
    {
        var result = await _mediator.Send(new GetAllBloodDonationQuery());
        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllBloodDonationsByUserId([FromQuery] string userId)
    {
        Guid id = Guid.Parse(userId);
        var result = await _mediator.Send(new GetByUserIdBloodDonationQuery(id) );
        return Ok(result);
    }
}