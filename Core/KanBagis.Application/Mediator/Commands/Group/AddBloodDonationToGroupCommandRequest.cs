using KanBagis.Application.Mediator.Results.BloodDonations;
using MediatR;

namespace KanBagis.Application.Mediator.Commands.Group;

public class AddBloodDonationToGroupCommandRequest : IRequest<AddBloodDonationToGroupCommandResult>
{
    public Guid BloodDonationId { get; set; }
    public Guid GroupId { get;set; } = Guid.Empty;
    public bool Public { get; set; }
    
    
}