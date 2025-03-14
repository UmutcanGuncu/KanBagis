using ClosedXML.Excel;
using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.DTOs;
using KanBagis.Domain.Entities;
using KanBagis.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

    public async Task<AddDistrictResultDTO> AddDistirctsWithExcelAsync(IFormFile file)
    {
        using var stream = new MemoryStream();
        file.CopyTo(stream);
        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RowsUsed();
        var districts = new List<District>();
        foreach (var row in rows.Skip(1))
        {
            string cityName  =row.Cell(1).Value.ToString();
            var cityResult = await  _context.Cities.FirstOrDefaultAsync(x => x.Name.Contains(cityName));
            if (cityResult == null)
            {
                return new ()
                {
                    Success = false,
                    Message = "Şehir ya da Hastane Bilgisi Bulunamadı",
                };
            }
            else
            {
                var district = new District()
                {
                    Id = Guid.NewGuid(),
                    Name = row.Cell(2).Value.ToString(),
                    CityId = cityResult.Id,
                    CreatedDate = DateTime.UtcNow

                };
                await _context.Districts.AddAsync(district);
                await _context.SaveChangesAsync();
            }
            
        }

        return new()
        {
            Success = true,
            Message = "Districts Added"
        };
    }
}