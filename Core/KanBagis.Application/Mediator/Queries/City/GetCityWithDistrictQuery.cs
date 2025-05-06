using KanBagis.Application.Mediator.Results.City;
using MediatR;

namespace KanBagis.Application.Mediator.Queries.City;

public class GetCityWithDistrictQuery : IRequest<GetCityWithDistrictResult>
{
    public GetCityWithDistrictQuery(string cityName)
    {
        CityName = cityName;
    }
    public string CityName { get; set; }
}