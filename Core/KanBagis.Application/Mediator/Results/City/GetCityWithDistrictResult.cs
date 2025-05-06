using KanBagis.Application.DTOs;

namespace KanBagis.Application.Mediator.Results.City;

public class GetCityWithDistrictResult
{
    public Guid CityId { get; set; }
    public string CityName { get; set; }
    public IEnumerable<DistrictDto> Districts { get; set; }
}