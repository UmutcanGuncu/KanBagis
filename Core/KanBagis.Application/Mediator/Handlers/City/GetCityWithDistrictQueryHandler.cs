using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.DTOs;
using KanBagis.Application.Mediator.Queries.City;
using KanBagis.Application.Mediator.Results.City;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.City;

public class GetCityWithDistrictQueryHandler(ICityService _cityService) : IRequestHandler<GetCityWithDistrictQuery, GetCityWithDistrictResult>
{
    public async Task<GetCityWithDistrictResult> Handle(GetCityWithDistrictQuery request, CancellationToken cancellationToken)
    {
        var result = await  _cityService.GetCityWithDistrictAsync(request.CityName);
        return new()
        {
            CityId = result.CityId,
            CityName = result.CityName,
            Districts = result.Districts
        };
    }
}