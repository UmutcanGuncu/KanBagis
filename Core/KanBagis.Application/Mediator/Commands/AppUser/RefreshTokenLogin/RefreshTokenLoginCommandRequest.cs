using KanBagis.Application.Mediator.Results.AppUser.RefreshTokenLogin;
using MediatR;

namespace KanBagis.Application.Mediator.Commands.AppUser.RefreshTokenLogin;

public class RefreshTokenLoginCommandRequest: IRequest<RefreshTokenLoginCommandResponse>
{
    public string RefreshToken { get; set; }
}