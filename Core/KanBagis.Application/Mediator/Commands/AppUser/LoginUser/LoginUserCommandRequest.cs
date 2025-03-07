using KanBagis.Application.Mediator.Results.AppUser.LoginUser;
using MediatR;

namespace KanBagis.Application.Mediator.Commands.AppUser.LoginUser;

public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}