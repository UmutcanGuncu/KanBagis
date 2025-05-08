using KanBagis.Application.Mediator.Results.BloodDonations;
using MediatR;

namespace KanBagis.Application.Mediator.Queries.Group;

public class GetGroupBySupervisorIdQuery : IRequest<IEnumerable<GetGroupBySupervisorIdResult>>
{
    public Guid SupervisorId { get; set; }
}