using KanBagis.Application.Mediator.Results.UserInformation;
using MediatR;

namespace KanBagis.Application.Mediator.Commands.UserInformation;

public class UpdateUserInformationCommandRequest : IRequest<UpdateUserInformationCommandResponse>
{
    public string UserId { get; set; }
    public string City { get; set; } // İl
    public string District { get; set; } // İlçe
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string OldPassword { get; set; }
    public string? NewPassword { get; set; }
}