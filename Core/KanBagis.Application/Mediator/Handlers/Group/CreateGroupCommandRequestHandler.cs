using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.Group;
using KanBagis.Application.Mediator.Results.BloodDonations;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.Group;

public class CreateGroupCommandRequestHandler(IGroupService _groupService) : IRequestHandler<CreateGroupCommandRequest, CreateGroupCommandResult>
{
    public async Task<CreateGroupCommandResult> Handle(CreateGroupCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _groupService.CreateGroupAsync(new()
        {
            Name = request.Name,
            SupervisorId = request.SupervisorId
        });
        return new()
        {
            Success = result.Success,
            Message = result.Message
        };
    }
}