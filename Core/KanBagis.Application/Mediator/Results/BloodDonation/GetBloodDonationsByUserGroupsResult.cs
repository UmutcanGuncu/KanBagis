using KanBagis.Domain.Enums;

namespace KanBagis.Application.Mediator.Results.BloodDonations;

public class GetBloodDonationsByUserGroupsResult
{
    public string NameSurname { get; set; }
    public string Phone { get; set; }
    public string BloodGroup { get; set; }
    public string Age { get; set; }
    public string Gender { get; set; }
    public string CityDistrict { get; set; }
    public string HospitalName { get; set; }
    public string Description { get; set; }
    public DonationStatus DonationStatus { get; set; }
    public DateTime CreateDate { get; set; }
}