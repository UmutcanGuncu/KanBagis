using KanBagis.Domain.Common;

namespace KanBagis.Domain.Entities;

public class Hospital : BaseEntity
{
    public Guid CityId { get; set; }
    public City City { get; set; }
    public Guid DistrictId { get; set; }
    public District District { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public ICollection<BloodDonation>  BloodDonation { get; set; }
}