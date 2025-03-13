using KanBagis.Domain.Common;

namespace KanBagis.Domain.Entities;

public class City : BaseEntity
{
    public string Name { get; set; }
    public ICollection<District> Districts { get; set; }
}