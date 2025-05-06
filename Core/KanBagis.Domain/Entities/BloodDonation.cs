using KanBagis.Domain.Common;
using KanBagis.Domain.Enums;

namespace KanBagis.Domain.Entities;

public class BloodDonation : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string BloodGroup { get; set; }
    public string Age { get; set; }
    public string Gender { get; set; }
    public Guid HospitalId { get; set; }
    public Hospital Hospital { get; set; }
    public string Description { get; set; }
    public DonationStatus DonationStatus { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser CreatedUser { get; set; }
    public ICollection<Group> Groups { get; set; }
}