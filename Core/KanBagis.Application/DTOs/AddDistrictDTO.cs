using KanBagis.Domain.Entities;

namespace KanBagis.Application.DTOs;

public class AddDistrictDTO
{
    public Guid CityId { get; set; }
    public string Name { get; set; }
}