namespace KanBagis.Application.Mediator.Results.UserInformation;

public class GetUserInformationByEmailQueryResult
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string District { get; set; }
}