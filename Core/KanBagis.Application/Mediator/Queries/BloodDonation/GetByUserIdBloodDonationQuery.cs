using KanBagis.Application.Mediator.Results.BloodDonation;
using MediatR;

namespace KanBagis.Application.Mediator.Queries.BloodDonation;

public class GetByUserIdBloodDonationQuery: IRequest<IEnumerable<GetByUserIdBloodDonationQueryResult>>
{
    public GetByUserIdBloodDonationQuery(Guid userId)
    {
        UserId = userId;
    }
    public Guid UserId { get; set; }
}