using KanBagis.Application.Mediator.Results.BloodDonations;
using MediatR;

namespace KanBagis.Application.Mediator.Commands.Group;

public class AddUserToGroupCommandRequest : IRequest<AddUserToGroupCommandResult>
{
    public Guid GroupId { get;set; }
    public string Email { get;set; }
    public Guid SupervisorId { get;set; }
    
}