using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.Group;
using KanBagis.Application.Mediator.Results.Group;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.Group;

public class AddBloodDonationToGroupCommandRequestHandler(IGroupService _service) : IRequestHandler<AddBloodDonationToGroupCommandRequest, AddBloodDonationToGroupCommandResult>
{
    public async Task<AddBloodDonationToGroupCommandResult> Handle(AddBloodDonationToGroupCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.AddBloodDonationToGroupAsync(request.BloodDonationId,request.GroupId);
        return new()
        {
            Success = result.Success,
            Message = result.Message,
        };
    }
}