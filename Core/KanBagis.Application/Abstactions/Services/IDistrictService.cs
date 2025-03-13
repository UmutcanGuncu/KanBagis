using KanBagis.Application.DTOs;

namespace KanBagis.Application.Abstactions.Services;

public interface IDistrictService
{
    public Task<AddDistrictResultDTO> AddDistrictsAsync(IEnumerable<AddDistrictDTO> addDistrictDto);
}