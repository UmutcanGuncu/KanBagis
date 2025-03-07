using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.AppUser.LoginUser;
using KanBagis.Application.Mediator.Results.AppUser.LoginUser;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
   private readonly IAuthService _authService;

   public LoginUserCommandHandler(IAuthService authService)
   {
       _authService = authService;
   }

   public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
       
       var result = await _authService.LoginAsync(request.Email, request.Password );
       return new()
       {
           Succeeded = result.Succeeded,
           Message = result.Message,
           Token = result.Token
       };
    }
}