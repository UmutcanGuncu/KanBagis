using KanBagis.Application.Abstactions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanBagis.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,User")]
public class UserController(IUserOperationService _userOperationService) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetUserInformation(string userId)
    {
        var userDto = await _userOperationService.GetUserInformation(userId);
        return Ok(userDto);
    }
}