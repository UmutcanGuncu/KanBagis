using KanBagis.Application.Mediator.Results.AppUser.CreateUser;
using MediatR;

namespace KanBagis.Application.Mediator.Commands.AppUser.CreateUser;

public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string BloodGroup { get; set; }
    public string Gender { get; set; }
    public string Age { get; set; }
    public string City { get; set; }
    public string District { get; set; } 
}