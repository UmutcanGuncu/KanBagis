using KanBagis.Application.DTOs;
using KanBagis.Domain.Entities;

namespace KanBagis.Application.Abstactions.Services;

public interface ICityService
{
    public Task<AddCityResultDTO> AddCityAsync(CityDTO cityDto);
    public Task<GetCityWithDistrictResultDTO>  GetCityWithDistrictAsync(string cityName);
}