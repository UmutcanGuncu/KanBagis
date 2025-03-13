using KanBagis.Domain.Common;

namespace KanBagis.Domain.Entities;

public class District : BaseEntity
{
    public string Name { get; set; }
    public Guid CityId { get; set; }
    public City City { get; set; }
}