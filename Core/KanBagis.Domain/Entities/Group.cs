using KanBagis.Domain.Common;

namespace KanBagis.Domain.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }
    public Guid SupervisorId { get; set; }
    public ICollection<AppUser> Users { get; set; }
    public ICollection<BloodDonation>  BloodDonations { get; set; }
}