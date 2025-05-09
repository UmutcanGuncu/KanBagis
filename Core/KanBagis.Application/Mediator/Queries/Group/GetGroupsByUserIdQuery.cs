using KanBagis.Application.Mediator.Results.BloodDonations;
using MediatR;

namespace KanBagis.Application.Mediator.Queries.Group;

public class GetGroupsByUserIdQuery : IRequest<IEnumerable<GetGroupsByUserIdResult>>
{
    public Guid UserId { get; set; }
}