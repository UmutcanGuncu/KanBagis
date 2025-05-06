using KanBagis.Application.Mediator.Results.Group;
using MediatR;

namespace KanBagis.Application.Mediator.Queries.Group;

public class GetGroupBySupervisorIdQuery : IRequest<IEnumerable<GetGroupBySupervisorIdResult>>
{
    public Guid SupervisorId { get; set; }
}