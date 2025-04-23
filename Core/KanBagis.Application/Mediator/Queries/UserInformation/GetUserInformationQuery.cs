using KanBagis.Application.Mediator.Results.UserInformation;
using MediatR;

namespace KanBagis.Application.Mediator.Queries.UserInformation;

public class GetUserInformationQuery : IRequest<GetUserInformationQueryResult>
{
    public string UserId { get; set; }
}