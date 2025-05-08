using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.Group;
using KanBagis.Application.Mediator.Results.BloodDonations;
using MediatR;


namespace KanBagis.Application.Mediator.Handlers.Group;

public class AddUserToGroupCommandRequestHandler(IGroupService _service):IRequestHandler<AddUserToGroupCommandRequest, AddUserToGroupCommandResult>
{
    public async Task<AddUserToGroupCommandResult> Handle(AddUserToGroupCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.AddUserToGroupAsync(request.GroupId, request.UserId, request.SupervisorId);
        return new()
        {
            Success = result.Succeeded,
            Message = result.Message
        };
    }
}