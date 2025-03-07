using Microsoft.AspNetCore.Identity;

namespace KanBagis.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BloodGroup { get; set; }
    public string Gender { get; set; }
    public string Age { get; set; }
    public string City { get; set; } // İl
    public string District { get; set; } // İlçe
    public bool IsActive { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public ICollection<BloodDonation> BloodDonations { get; set; }
}