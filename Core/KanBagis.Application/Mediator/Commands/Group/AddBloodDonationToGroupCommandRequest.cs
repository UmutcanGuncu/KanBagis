using KanBagis.Application.Mediator.Results.Group;
using MediatR;

namespace KanBagis.Application.Mediator.Commands.Group;

public class AddBloodDonationToGroupCommandRequest : IRequest<AddBloodDonationToGroupCommandResult>
{
    public Guid BloodDonationId { get; set; }
    public Guid GroupId { get;set; }
}