using KanBagis.Application.DTOs;

namespace KanBagis.Application.Mediator.Results.AppUser.LoginUser;

public class LoginUserCommandResponse
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public string UserId { get; set; }
    public Token Token { get; set; }
    
}