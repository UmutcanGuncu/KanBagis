using KanBagis.Application.Mediator.Results.BloodDonation;
using MediatR;

namespace KanBagis.Application.Mediator.Commands.BloodDonation;

public class ChangeBloodDonationStatusAdminCommandRequest : IRequest<ChangeBloodDonationStatusAdminCommandResult>
{
    public Guid BloodDonationId { get; set; }
    public bool Status { get; set; }
}