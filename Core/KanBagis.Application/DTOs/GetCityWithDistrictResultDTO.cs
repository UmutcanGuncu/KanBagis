using KanBagis.Domain.Entities;

namespace KanBagis.Application.DTOs;

public class GetCityWithDistrictResultDTO
{
    public Guid CityId { get; set; }
    public string CityName { get; set; }
    public IEnumerable<DistrictDto> Districts { get; set; }
}