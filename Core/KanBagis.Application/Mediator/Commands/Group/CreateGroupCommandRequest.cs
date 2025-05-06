using KanBagis.Application.Mediator.Results.Group;
using MediatR;

namespace KanBagis.Application.Mediator.Commands.Group;

public class CreateGroupCommandRequest : IRequest<CreateGroupCommandResult>
{
    public string Name { get; set; }
    public Guid SupervisorId { get; set; }
}