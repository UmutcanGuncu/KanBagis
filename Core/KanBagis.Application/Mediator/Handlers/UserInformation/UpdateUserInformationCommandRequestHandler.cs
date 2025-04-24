using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.UserInformation;
using KanBagis.Application.Mediator.Results.UserInformation;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.UserInformation;

public class UpdateUserInformationCommandRequestHandler(IUserOperationService _operationService) : IRequestHandler<UpdateUserInformationCommandRequest,UpdateUserInformationCommandResponse>
{
    public async Task<UpdateUserInformationCommandResponse> Handle(UpdateUserInformationCommandRequest request, CancellationToken cancellationToken)
    {
        var value = await _operationService.UpdateUserInformation(new()
        {
            UserId = request.UserId,
            Email = request.Email,
            City = request.City,
            District = request.District,
            NewPassword = request.NewPassword,
            OldPassword = request.OldPassword,
            PhoneNumber = request.PhoneNumber,
        });
        return new()
        {
            UserId = value.UserId,
            Message = value.Message,
            Success = value.Success,
        };
    }
}