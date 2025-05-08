using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Queries.Group;
using KanBagis.Application.Mediator.Results.BloodDonations;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.Group;

public class GetGroupBySupervisorIdQueryHandler(IGroupService _service): IRequestHandler<GetGroupBySupervisorIdQuery, IEnumerable<GetGroupBySupervisorIdResult>>
{
    public async Task<IEnumerable<GetGroupBySupervisorIdResult>> Handle(GetGroupBySupervisorIdQuery request, CancellationToken cancellationToken)
    {
        var results = await _service.GetGroupsBySupervisorIdAsync(request.SupervisorId);
        return results.Select(x => new GetGroupBySupervisorIdResult()
        {
            GroupId = x.Id,
            SupervisorId = x.SupervisorId,
            Name = x.Name,
        });
    }
}