using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.Group;
using KanBagis.Application.Mediator.Results.BloodDonations;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.Group;

public class AddBloodDonationToGroupCommandRequestHandler(IGroupService _service) : IRequestHandler<AddBloodDonationToGroupCommandRequest, AddBloodDonationToGroupCommandResult>
{
    public async Task<AddBloodDonationToGroupCommandResult> Handle(AddBloodDonationToGroupCommandRequest request, CancellationToken cancellationToken)
    {
        if (!request.Public)
        {
            var result = await _service.AddBloodDonationToGroupAsync(request.BloodDonationId,request.GroupId);
            return new()
            {
                Success = result.Success,
                Message = result.Message,
            };
        }
        var result2 = await _service.AddBloodDonationToGroupAsync(request.BloodDonationId);
        return new()
        {
            Success = result2.Success,
            Message = result2.Message,
        };
         
        
    }
}