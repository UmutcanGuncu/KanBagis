using KanBagis.Application.Mediator.Results.Group;
using MediatR;

namespace KanBagis.Application.Mediator.Queries.Group;

public class GetGroupsByUserIdQuery : IRequest<IEnumerable<GetGroupsByUserIdResult>>
{
    public Guid UserId { get; set; }
}