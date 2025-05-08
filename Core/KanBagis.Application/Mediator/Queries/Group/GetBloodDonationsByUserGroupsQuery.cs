using KanBagis.Application.Mediator.Results.BloodDonations;
using MediatR;

namespace KanBagis.Application.Mediator.Queries.Group;

public class GetBloodDonationsByUserGroupsQuery : IRequest<IEnumerable<GetBloodDonationsByUserGroupsResult>>
{
    public Guid UserId { get; set; }
}