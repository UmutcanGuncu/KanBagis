using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.DTOs;
using KanBagis.Application.DTOs.BloodDonation.Response;
using KanBagis.Application.Settings;
using KanBagis.Domain.Enums;
using KanBagis.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace KanBagis.Persistence.Services;

public class BloodDonationService(KanBagisDbContext _context, IGroupService _groupService) : IBloodDonationService
{
    public async Task<AddBloodDonationDTO> CreateAsync(BloodDonationDTO bloodDonationDto)
    {
        Guid bloodDonationId = Guid.NewGuid();
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
                Id = bloodDonationId,
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
            await _groupService.AddBloodDonationToGroupAsync(bloodDonationId, bloodDonationDto.GroupId);
            return new()
            {
                Success = true,
                Message = "Bağış Talebiniz Başarıyla Kaydedildi",
                BloodDonationId = bloodDonationId
            };
        }

        var result = await _context.BloodDonations.AddAsync(new()
        {
            Id = bloodDonationId,
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
                Message = "Bağış Talebiniz Başarıyla Kaydedildi",
                BloodDonationId = bloodDonationId
            };
        
        
    }

    public async Task<IEnumerable<GetAllBloodDonationResponseDTO>> GetPublicAllAsync()
    {
        var values = await _context.BloodDonations.
            Include(x=>x.Hospital)
            .ThenInclude(x=>x.City)
            .Include(x=>x.Hospital)
            .ThenInclude(x=>x.District)
            .Include(x=>x.Groups)
            .Where(x=>x.DonationStatus == DonationStatus.Onaylandı)
            .Where(x=>x.Groups.Any(g=>g.Id == AppGuids.PublicGroupId))
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
            Description = x.Description,
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
            Description = x.Description,
            CreateDate = x.CreatedDate
        }).OrderByDescending(x=>x.CreateDate).ToList();
        return resultDto;
    }

    public async Task<IEnumerable<GetFilteredBloodDonationResponseDTO>> GetPublicFilteredAsync(string city = null, string district = null, string hospitalName = null)
    {
        var query = _context.BloodDonations.Include(x=>x.Hospital)
            .ThenInclude(x=>x.City)
            .Include(x=>x.Hospital)
            .ThenInclude(x=>x.District).Where(x=>x.DonationStatus == DonationStatus.Onaylandı)
            .Include(x=>x.Groups).AsQueryable();
        if (!string.IsNullOrEmpty(city))
        {
            query = query.Where(x=>x.Hospital.City.Name.Equals(city) && x.Groups.Any(g=>g.Id == AppGuids.PublicGroupId));
        }

        if (!string.IsNullOrEmpty(district))
        {
            query = query.Where(x=>x.Hospital.District.Name.Equals(district) && x.Groups.Any(g=>g.Id == AppGuids.PublicGroupId));
        }

        if (!string.IsNullOrEmpty(hospitalName))
        {
            query = query.Where(x=>x.Hospital.Name.Contains(hospitalName) && x.Groups.Any(g=>g.Id == AppGuids.PublicGroupId));
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
            Description = x.Description,
            CreateDate = x.CreatedDate
        }).OrderByDescending(x=>x.CreateDate).ToList();
        return resultDto;
    }

    public async Task<bool> ChangeBloodDonationStatusAdminAsync(Guid bloodDonationId,  bool status)
    {
        var result = await _context.BloodDonations.Where(x=> x.Id == bloodDonationId).FirstOrDefaultAsync();
        if (result != null)
        {
            if (status)
            {
                result.DonationStatus = DonationStatus.Onaylandı;
                result.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            result.DonationStatus = DonationStatus.Onaylanmadı;
            result.ModifiedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
       
    }

    public async Task<IEnumerable<GetBloodDonationsByUserGroupsResponseDto>> GetBloodDonationsByUserGroupsAsync(Guid userId)
    {
       var donations = await _context.Groups
           .Where(g => g.Users.Any(u => u.Id == userId) && g.Id != AppGuids.PublicGroupId)
           .SelectMany(g => g.BloodDonations)
           .Include(b => b.Hospital)
           .ThenInclude(h => h.City)
           .ThenInclude(c => c.Districts)
           .ToListAsync();
       return donations.Select(x => new GetBloodDonationsByUserGroupsResponseDto()
       {
           NameSurname = x.Name + " " + x.Surname,
           Phone = x.PhoneNumber,
           BloodGroup = x.BloodGroup,
           Age = x.Age,
           Gender = x.Gender,
           HospitalName = x.Hospital.Name,
           CityDistrict = x.Hospital.City.Name,
           DonationStatus = x.DonationStatus,
           Description = x.Description,
           CreateDate = x.CreatedDate
       });

    }

    public async Task<UpdateBloodDonationResultDto> UpdateBloodDonationAsync(UpdateBloodDonationDto bloodDonationDto)
    {
        throw new NotImplementedException();
    }
}