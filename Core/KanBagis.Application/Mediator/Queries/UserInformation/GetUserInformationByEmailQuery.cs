using KanBagis.Application.Mediator.Results.UserInformation;
using MediatR;

namespace KanBagis.Application.Mediator.Queries.UserInformation;

public class GetUserInformationByEmailQuery : IRequest<GetUserInformationByEmailQueryResult>
{
    public string Email { get; set; }
}