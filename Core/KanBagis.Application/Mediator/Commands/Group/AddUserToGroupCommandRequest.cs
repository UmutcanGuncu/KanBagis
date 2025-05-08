using KanBagis.Application.Mediator.Results.BloodDonations;
using MediatR;

namespace KanBagis.Application.Mediator.Commands.Group;

public class AddUserToGroupCommandRequest : IRequest<AddUserToGroupCommandResult>
{
    public Guid GroupId { get;set; }
    public Guid UserId { get;set; }
    public Guid SupervisorId { get;set; }
    
}