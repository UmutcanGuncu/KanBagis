using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.DTOs;
using KanBagis.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace KanBagis.Persistence.Services;

public class CityService(KanBagisDbContext _context) : ICityService
{
    
    public async Task<AddCityResultDTO> AddCityAsync(CityDTO cityDto)
    {
        var result =  await _context.Cities.Where(x => x.Name.Equals(cityDto.Name)).FirstOrDefaultAsync();
        if (result != null)
            return new()
            {
                Success = false,
                Message = $"{cityDto.Name} Var Olduğundan Eklenemedi"
            };
        await _context.Cities.AddAsync(new()
        {
            Name = cityDto.Name,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();
        return new()
        {
            Success = true,
            Message = $"{cityDto.Name} Şehri Başarıyla Eklendi ",
        };

    }

    public async Task<GetCityWithDistrictResultDTO> GetCityWithDistrictAsync(string cityName)
    {
        var city = await _context.Cities.Include(x=>x.Districts).Where(x => x.Name.Equals(cityName)).FirstOrDefaultAsync();
        if (city != null)
        {
            var districts = await _context.Districts.Where(x=> x.CityId.Equals(city.Id)).ToListAsync();
            IEnumerable<DistrictDto> districtDtos = districts.Select(d => new DistrictDto
            {
                DistrictId = d.Id,
                Name = d.Name,
            });
            return new()
            {
                CityId = city.Id,
                CityName = city.Name,
                Districts = districtDtos,
            };
        }

        return new();
    }
}