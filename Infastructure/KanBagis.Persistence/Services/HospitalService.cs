using System.Globalization;
using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.DTOs;
using KanBagis.Persistence.Contexts;
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
}