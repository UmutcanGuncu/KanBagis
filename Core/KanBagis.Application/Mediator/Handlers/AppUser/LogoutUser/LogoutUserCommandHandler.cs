using KanBagis.Application.Mediator.Commands.AppUser.LoginUser;
using KanBagis.Application.Mediator.Commands.AppUser.LogoutUser;
using KanBagis.Application.Mediator.Results.AppUser.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KanBagis.Application.Mediator.Handlers.AppUser.LogoutUser;

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommandRequest>
{
    private readonly SignInManager<Domain.Entities.AppUser> _signInManager;

    public LogoutUserCommandHandler(SignInManager<Domain.Entities.AppUser> signInManager)
    {
        _signInManager = signInManager;
    }


    public async Task Handle(LogoutUserCommandRequest request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
    }
}