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

public class BloodDonationController(IMediator _mediator): ControllerBase
{
    [Authorize(Roles = "Admin,User")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddBloodDonation(CreateBloodDonationCommandRequest createBloodDonationCommandRequest)
    {
        var result = await _mediator.Send(createBloodDonationCommandRequest);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllBloodDonations()
    {
        var result = await _mediator.Send(new GetAllBloodDonationQuery());
        return Ok(result);
    }
    [Authorize(Roles = "Admin,User")]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllBloodDonationsByUserId([FromQuery] string userId)
    {
        Guid id = Guid.Parse(userId);
        var result = await _mediator.Send(new GetByUserIdBloodDonationQuery(id) );
        return Ok(result);
    }
    [Authorize(Roles = "Admin,User")]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetFilteredBloodDonations([FromQuery] string city = null,
        [FromQuery] string district = null, [FromQuery] string hospitalName = null)
    {
        var result = await _mediator.Send(new GetFilteredBloodDonationQuery(city, district, hospitalName));
        return Ok(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("[action]")]
    public async Task<IActionResult> ChangeBloodDonationStatusAdmin(Guid bloodDonationId, bool status)
    {
        var result = await _mediator.Send(new ChangeBloodDonationStatusAdminCommandRequest()
        {
            BloodDonationId = bloodDonationId,
            Status = status
        });
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}