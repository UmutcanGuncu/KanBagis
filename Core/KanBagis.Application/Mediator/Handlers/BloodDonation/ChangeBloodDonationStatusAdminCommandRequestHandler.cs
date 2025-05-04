using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.BloodDonation;
using KanBagis.Application.Mediator.Results.BloodDonation;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.BloodDonation;

public class ChangeBloodDonationStatusAdminCommandRequestHandler(IBloodDonationService _bloodDonation) : IRequestHandler<ChangeBloodDonationStatusAdminCommandRequest, ChangeBloodDonationStatusAdminCommandResult>
{
    public async Task<ChangeBloodDonationStatusAdminCommandResult> Handle(ChangeBloodDonationStatusAdminCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _bloodDonation.ChangeBloodDonationStatusAdminAsync(request.BloodDonationId, request.Status);
        return new()
        {
            Success = result
        };
    }
}