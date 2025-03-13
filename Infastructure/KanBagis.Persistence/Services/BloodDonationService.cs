using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.DTOs;
using KanBagis.Application.DTOs.BloodDonation.Response;
using KanBagis.Domain.Enums;
using KanBagis.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace KanBagis.Persistence.Services;

public class BloodDonationService(KanBagisDbContext _context) : IBloodDonationService
{
    public async Task<AddBloodDonationDTO> CreateAsync(BloodDonationDTO bloodDonationDto)
    {
        if (bloodDonationDto.CurrentUser == true)
        {
            var currentUser = await _context.Users.FindAsync(bloodDonationDto.AppUserId);
            if (currentUser == null)
                return new()
                {
                    Success = false,
                    Message = "İlgili Kullanıcı Bulunamadı"
                };
            await _context.BloodDonations.AddAsync(new()
            {
                Name = currentUser.FirstName,
                Surname = currentUser.LastName,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                Age = currentUser.Age,
                Gender = currentUser.Gender,
                AppUserId = currentUser.Id,
                BloodGroup = currentUser.BloodGroup,
                CreatedDate = DateTime.UtcNow,
                HospitalId = bloodDonationDto.HospitalId,
                Description = bloodDonationDto.Description,
                DonationStatus = DonationStatus.OnayBekliyor
            });
            await _context.SaveChangesAsync();
            return new()
            {
                Success = true,
                Message = "Bağış Talebiniz Başarıyla Kaydedildi"
            };
        }

        var result = await _context.BloodDonations.AddAsync(new()
        {
            Name = bloodDonationDto.Name,
            Surname = bloodDonationDto.Surname,
            Email = bloodDonationDto.Email,
            PhoneNumber = bloodDonationDto.PhoneNumber,
            Age = bloodDonationDto.Age,
            Gender = bloodDonationDto.Gender,
            AppUserId = bloodDonationDto.AppUserId,
            BloodGroup = bloodDonationDto.BloodGroup,
            CreatedDate = DateTime.UtcNow,
            HospitalId = bloodDonationDto.HospitalId,
            Description = bloodDonationDto.Description,
            DonationStatus = DonationStatus.OnayBekliyor
        });
        await _context.SaveChangesAsync();
            return new()
            {
                Success = true,
                Message = "Bağış Talebiniz Başarıyla Kaydedildi"
            };
        
        
    }

    public async Task<IEnumerable<GetAllBloodDonationResponseDTO>> GetAllAsync()
    {
        var values = await _context.BloodDonations.
            Include(x=>x.Hospital)
            .ThenInclude(x=>x.City)
            .Include(x=>x.Hospital)
            .ThenInclude(x=>x.District)
            .ToListAsync();
        var resultDto = values.Select(x => new GetAllBloodDonationResponseDTO()
        {
            NameSurname = x.Name + " " + x.Surname,
            Phone = x.PhoneNumber,
            BloodGroup = x.BloodGroup,
            Age = x.Age,
            Gender = x.Gender,
            HospitalName = x.Hospital.Name,
            CityDistrict = x.Hospital.City.Name + " " + x.Hospital.District.Name,
            DonationStatus = x.DonationStatus,
            CreateDate = x.CreatedDate
        }).OrderByDescending(x=>x.CreateDate).ToList();
        return resultDto;
    }

    public async Task<IEnumerable<GetUserIdBloodDonationResponseDTO>> GetByUserIdAsync(Guid userId)
    {
        var values = await _context.BloodDonations.Where(x => x.CreatedUser.Id == userId )
            .Include(x => x.Hospital)
            .ThenInclude(x => x.City)
            .Include(x => x.Hospital)
            .ThenInclude(x => x.District)
            .ToListAsync();
        var resultDto = values.Select(x => new GetUserIdBloodDonationResponseDTO()
        {
            NameSurname = x.Name + " " + x.Surname,
            Phone = x.PhoneNumber,
            BloodGroup = x.BloodGroup,
            Age = x.Age,
            Gender = x.Gender,
            HospitalName = x.Hospital.Name,
            CityDistrict = x.Hospital.City.Name + " " + x.Hospital.District.Name,
            DonationStatus = x.DonationStatus,
            CreateDate = x.CreatedDate
        }).OrderByDescending(x=>x.CreateDate).ToList();
        return resultDto;
    }

    public async Task<IEnumerable<GetFilteredBloodDonationResponseDTO>> GetFiltederAsync(string city = null, string district = null, string hospitalName = null)
    {
        var query = _context.BloodDonations.Include(x=>x.Hospital)
            .ThenInclude(x=>x.City)
            .Include(x=>x.Hospital)
            .ThenInclude(x=>x.District).Where(x=>x.DonationStatus == DonationStatus.Onaylandı).AsQueryable();
        if (!string.IsNullOrEmpty(city))
        {
            query = query.Where(x=>x.Hospital.City.Name.Equals(city));
        }

        if (!string.IsNullOrEmpty(district))
        {
            query = query.Where(x=>x.Hospital.District.Name.Equals(district));
        }

        if (!string.IsNullOrEmpty(hospitalName))
        {
            query = query.Where(x=>x.Hospital.Name.Contains(hospitalName));
        }
        var values = await query.ToListAsync();
        var resultDto = values.Select(x => new GetFilteredBloodDonationResponseDTO()
        {
            NameSurname = x.Name + " " + x.Surname,
            Phone = x.PhoneNumber,
            BloodGroup = x.BloodGroup,
            Age = x.Age,
            Gender = x.Gender,
            HospitalName = x.Hospital.Name,
            CityDistrict = x.Hospital.City.Name + " " + x.Hospital.District.Name,
            DonationStatus = x.DonationStatus,
            CreateDate = x.CreatedDate
        }).OrderByDescending(x=>x.CreateDate).ToList();
        return resultDto;
    }
}