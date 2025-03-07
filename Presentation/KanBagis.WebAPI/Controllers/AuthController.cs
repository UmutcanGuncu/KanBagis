using KanBagis.Application.Mediator.Commands.AppUser.CreateUser;
using KanBagis.Application.Mediator.Commands.AppUser.LoginUser;
using KanBagis.Application.Mediator.Commands.AppUser.LogoutUser;
using KanBagis.Application.Mediator.Commands.AppUser.RefreshTokenLogin;
using KanBagis.Application.Mediator.Results.AppUser.CreateUser;
using KanBagis.Application.Mediator.Results.AppUser.LoginUser;
using KanBagis.Application.Mediator.Results.AppUser.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanBagis.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
    {
        CreateUserCommandResponse response = await _mediator.Send(request);
        if (response.Succeeded)
            return Ok(response);
        return BadRequest(response);
    }
    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUserCommandRequest request)
    {
        LoginUserCommandResponse response = await _mediator.Send(request);
        if (response.Succeeded)
            return Ok(response);
        return BadRequest(response);
    }

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshTokenLogin([FromForm] RefreshTokenLoginCommandRequest request)
    {
        RefreshTokenLoginCommandResponse response = await _mediator.Send(request);
        return Ok();
    }
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        LogoutUserCommandRequest request = new LogoutUserCommandRequest();
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public IActionResult Deneme()
    {
        return Ok("Girdi");
    }
}