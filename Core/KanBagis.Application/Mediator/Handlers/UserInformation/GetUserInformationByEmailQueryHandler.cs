using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Queries.UserInformation;
using KanBagis.Application.Mediator.Results.UserInformation;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.UserInformation;

public class GetUserInformationByEmailQueryHandler(IUserOperationService _service) : IRequestHandler<GetUserInformationByEmailQuery, GetUserInformationByEmailQueryResult>
{
    public async Task<GetUserInformationByEmailQueryResult> Handle(GetUserInformationByEmailQuery request, CancellationToken cancellationToken)
    {
        var value = await _service.GetUserInformationByEmailAsync(request.Email);
        return new()
        {
            Id = value.Id,
            Email = request.Email,
            FirstName = value.FirstName,
            LastName = value.LastName,
            City = value.City,
            District = value.District,
        };
    }
}