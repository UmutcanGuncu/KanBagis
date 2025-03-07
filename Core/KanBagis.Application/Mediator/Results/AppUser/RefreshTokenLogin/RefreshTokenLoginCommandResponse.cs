using KanBagis.Application.DTOs;

namespace KanBagis.Application.Mediator.Results.AppUser.RefreshTokenLogin;

public class RefreshTokenLoginCommandResponse
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public Token Token { get; set; }
}