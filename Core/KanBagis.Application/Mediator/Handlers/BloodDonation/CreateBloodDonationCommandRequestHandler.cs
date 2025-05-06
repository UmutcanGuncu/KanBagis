using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.BloodDonation;
using KanBagis.Application.Mediator.Results.BloodDonation;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.BloodDonation;

public class CreateBloodDonationCommandRequestHandler(IBloodDonationService _bloodDonationService) : IRequestHandler<CreateBloodDonationCommandRequest, CreateBloodDonationCommandResult>
{
    public async Task<CreateBloodDonationCommandResult> Handle(CreateBloodDonationCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _bloodDonationService.CreateAsync(new()
        {
           CurrentUser = request.CurrentUser,
           Name = request.Name,
           Surname = request.Surname,
           PhoneNumber = request.PhoneNumber,
           Email = request.Email,
           BloodGroup = request.BloodGroup,
           Age = request.Age,
           Gender = request.Gender,
           Description = request.Description,
           HospitalId = request.HospitalId,
           AppUserId = request.AppUserId
            
        });
        return new()
        {
            Success = result.Success,
            Message = result.Message,
            BloodDonationId = result.BloodDonationId
        };
    }
}