using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.DTOs;
using KanBagis.Persistence.Contexts;

namespace KanBagis.Persistence.Services;

public class DistrictService(KanBagisDbContext _context) : IDistrictService
{
    public async Task<AddDistrictResultDTO> AddDistrictsAsync(IEnumerable<AddDistrictDTO> addDistrictDto)
    {
        foreach (var item in addDistrictDto)
        {
            var value =await _context.Districts.AddAsync(new()
            {
                CityId = item.CityId,
                Name = item.Name,
                CreatedDate = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }

        return new()
        {
            Success = true,
            Message = "Districts Added"
        };
    }
}