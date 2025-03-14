using System.Globalization;
using ClosedXML.Excel;
using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.DTOs;
using KanBagis.Domain.Entities;
using KanBagis.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KanBagis.Persistence.Services;

public class HospitalService(KanBagisDbContext _context) : IHospitalService
{
    public async Task<IEnumerable<HospitalDTO>> GetFilteredHospitalsAsync(string city = null, string district = null)
    {
        var query  = _context.Hospitals.Include(x=>x.City).Include(x => x.District).AsQueryable();
        if (!string.IsNullOrEmpty(city))
        {
            
            query = query.Where(x=> x.City.Name.Equals(city));

        }

        if (!string.IsNullOrEmpty(district))
        {
            query = query.Where(x => x.District.Name.Equals(district));
        }
        var values = await query.ToListAsync();
        var getFilteredHospitalsDto = values.Select(x=> new HospitalDTO()
        {
            Id = x.Id,
            Name = x.Name,
            Location = x.Location
        }).ToList();
        return getFilteredHospitalsDto;
    }

    public async Task<AddHospitalsResultDTO> AddHospitalsAsync(IFormFile file)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RowsUsed();
        var hospitals = new List<HospitalDTO>();
        foreach (var row in rows.Skip(1))
        {
            string cityName  =row.Cell(1).Value.ToString();
            string districtName = row.Cell(2).Value.ToString();
            var cityResult = await _context.Cities.FirstOrDefaultAsync(x => x.Name.Contains(cityName));
            var districtResult = await _context.Districts.FirstOrDefaultAsync(x => x.Name.Contains(districtName) && x.City.Id == cityResult.Id);
            if (cityResult == null)
            {
                await _context.Cities.AddAsync(new City()
                {
                    Name = cityName,
                    CreatedDate = DateTime.UtcNow,
                });
                cityResult =  await _context.Cities.FirstOrDefaultAsync(x => x.Name.Contains(cityName));
            }else if (districtResult == null)
            {
                await _context.Districts.AddAsync(new District()
                {
                    Name = districtName,
                    CityId = cityResult.Id,
                    CreatedDate = DateTime.UtcNow
                });
                await _context.SaveChangesAsync();
            }
            districtResult = await _context.Districts.Include(x=>x.City).FirstOrDefaultAsync(x => x.Name.Contains(districtName) && x.City.Id == cityResult.Id);
            var hospital = new Hospital()
            {
                    Id = Guid.NewGuid(),
                    Name = row.Cell(3).Value.ToString(),
                    CityId = cityResult.Id,
                    DistrictId = districtResult.Id,
                    CreatedDate = DateTime.UtcNow,
                    Location = "",

            };
            await _context.Hospitals.AddAsync(hospital);
            await _context.SaveChangesAsync();
            
            
        }

        return new AddHospitalsResultDTO()
        {
            Success = true,
            Message = "Hastane Bilgileri Başarıyla Eklendi",
            Hospitals = hospitals
        };
    }
}