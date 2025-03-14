using KanBagis.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace KanBagis.Application.Abstactions.Services;

public interface IDistrictService
{
    public Task<AddDistrictResultDTO> AddDistrictsAsync(IEnumerable<AddDistrictDTO> addDistrictDto);
    public Task<AddDistrictResultDTO> AddDistirctsWithExcelAsync(IFormFile file);
}