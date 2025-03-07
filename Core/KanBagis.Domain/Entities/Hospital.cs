using KanBagis.Domain.Common;

namespace KanBagis.Domain.Entities;

public class Hospital : BaseEntity
{
    public string City { get; set; }
    public string District { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public ICollection<BloodDonation>  BloodDonation { get; set; }
}