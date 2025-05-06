using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Queries.UserInformation;
using KanBagis.Application.Mediator.Results.UserInformation;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.UserInformation;

public class GetUserInformationQueryHandler(IUserOperationService _operationService) : IRequestHandler<GetUserInformationQuery, GetUserInformationQueryResult>
{
    public async Task<GetUserInformationQueryResult> Handle(GetUserInformationQuery request, CancellationToken cancellationToken)
    {
       var result = await _operationService.GetUserInformationAsync(request.UserId);
       return new()
       {
           Age = result.Age,
           BloodGroup = result.BloodGroup,
           City = result.City,
           District = result.District,
           Email = result.Email,
           FirstName = result.FirstName,
           Gender = result.Gender,
           LastName = result.LastName,
           Phone = result.Phone
       };
    }
}