using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.AppUser.RefreshTokenLogin;
using KanBagis.Application.Mediator.Results.AppUser.RefreshTokenLogin;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.AppUser.RefreshTokenLogin;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IRoleService _roleService;

    public RefreshTokenCommandHandler(IAuthService authService, IRoleService roleService)
    {
        _authService = authService;
        _roleService = roleService;
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