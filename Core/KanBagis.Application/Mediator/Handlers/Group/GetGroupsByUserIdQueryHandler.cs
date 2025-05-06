using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Queries.Group;
using KanBagis.Application.Mediator.Results.Group;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.Group;

public class GetGroupsByUserIdQueryHandler(IGroupService _groupService) : IRequestHandler<GetGroupsByUserIdQuery, IEnumerable<GetGroupsByUserIdResult>>
{
    public async Task<IEnumerable<GetGroupsByUserIdResult>> Handle(GetGroupsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var results = await _groupService.GetGroupsByUserIdAsync(request.UserId);
        return results.Select(x=> new GetGroupsByUserIdResult()
        {
            GroupId = x.Id,
            Name = x.Name,
            SupervisorId = x.SupervisorId
        });
    }
}