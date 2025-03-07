using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.AppUser.RefreshTokenLogin;
using KanBagis.Application.Mediator.Results.AppUser.RefreshTokenLogin;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.AppUser.RefreshTokenLogin;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
    {
       var result = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
       return new()
       {
           Message = result.Message,
           Succeeded = result.Succeeded,
           Token = result.Token
       };
    }
}