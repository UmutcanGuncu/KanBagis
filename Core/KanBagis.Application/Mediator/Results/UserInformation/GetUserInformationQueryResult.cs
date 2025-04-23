namespace KanBagis.Application.Mediator.Results.UserInformation;

public class GetUserInformationQueryResult
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BloodGroup { get; set; }
    public string Gender { get; set; }
    public string Age { get; set; }
    public string City { get; set; } // İl
    public string District { get; set; } // İlçe
    public string Email { get; set; }
    public string Phone { get; set; }
}