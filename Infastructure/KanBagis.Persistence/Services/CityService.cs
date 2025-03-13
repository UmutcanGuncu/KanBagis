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
}