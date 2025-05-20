using DocumentFormat.OpenXml.Office2010.Excel;
using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.DTOs;
using KanBagis.Application.Settings;
using KanBagis.Domain.Entities;
using KanBagis.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace KanBagis.Persistence.Services;

public class GroupService(KanBagisDbContext _context) : IGroupService
{
    public async Task<AddUserToGroupResultDto> AddUserToGroupAsync(Guid groupId, Guid userId)
    {
       var user = await _context.Users.Include(u => u.Groups).FirstOrDefaultAsync(u => u.Id == userId);
       var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
       if (user == null || group == null)
           return new AddUserToGroupResultDto()
           {
               Succeeded = false,
               Message = "Kullanıcı veya Grup Bilgisi Bulunamadı"
           };
       user.Groups.Add(group);
       await _context.SaveChangesAsync();
       return new AddUserToGroupResultDto()
       {
           Succeeded = true,
           Message = "Kullanıcı Başarıyla Gruba Eklendi"
       };
    }

    public async Task<AddUserToGroupResultDto> AddUserToGroupAsync(Guid groupId, string email, Guid supervisorId)
    {
        var user = await _context.Users.Include(u => u.Groups).FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return new AddUserToGroupResultDto()
            {
                Succeeded = false,
                Message = "Kullanıcı Bilgisi Bulunamadı"
            };
        }
        var group = await _context.Groups.FirstOrDefaultAsync(g => g.SupervisorId == supervisorId && g.Id == groupId);
        if (group == null)
            return new AddUserToGroupResultDto()
            {
                Succeeded = false,
                Message = "Gruba Kullanıcı Ekleme Yetkiniz Bulunmamaktadır"
            };
        user.Groups.Add(group);
        await _context.SaveChangesAsync();
        return new AddUserToGroupResultDto()
        {
            Succeeded = true,
            Message = "Kullanıcı Başarıyla Gruba Eklendi"
        };
    }

    public async Task<CreateGroupResultDto> CreateGroupAsync(CreateGroupDto createGroupDto)
    {
        var user = await _context.Users.Include(u=> u.Groups).FirstOrDefaultAsync(x=> x.Id == createGroupDto.SupervisorId);
        if (user == null)
            return new CreateGroupResultDto()
            {
                Success = false,
                Message = "Grup oluşturacak kullanıcı bulunamadı"
            };
        var groupId = Guid.NewGuid();
        await _context.Groups.AddAsync(new Group()
        {
            Id = groupId,
            Name = createGroupDto.Name,
            SupervisorId = user.Id,
            CreatedDate = DateTime.Now
        }); 
        await _context.SaveChangesAsync();
        var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
        user.Groups.Add(group);
        await _context.SaveChangesAsync();
        return new CreateGroupResultDto()
        {
            Success = true,
            Message = "Grup Başarıyla Oluşturuldu",
            GroupId = groupId
        };
    }

    public async Task<AddBloodDonationToGroupResultDto> AddBloodDonationToGroupAsync(Guid bloodDonationId, Guid groupId)
    {
        var bloodDonation = await _context.BloodDonations.Include(g => g.Groups).FirstOrDefaultAsync(x=> x.Id == bloodDonationId);
        var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
        if (bloodDonation == null || group == null)
            return new AddBloodDonationToGroupResultDto()
            {
                Success = false,
                Message = "Kan bağışı veya grup adı bulunamadı"
            };
        bloodDonation.Groups.Add(group);
        await _context.SaveChangesAsync();
        return new AddBloodDonationToGroupResultDto()
        {
            Success = true,
            Message = "İşlem Başarıyla Tamamlandı"
        };
    }
    // public gruba dahil etmek için kullanılır
    public async Task<AddBloodDonationToGroupResultDto> AddBloodDonationToGroupAsync(Guid bloodDonationId)
    {
        var bloodDonation = await _context.BloodDonations.Include(g => g.Groups).FirstOrDefaultAsync(x=> x.Id == bloodDonationId);
        var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == AppGuids.PublicGroupId);
        if (bloodDonation == null || group == null)
            return new AddBloodDonationToGroupResultDto()
            {
                Success = false,
                Message = "Kan bağışı veya grup adı bulunamadı"
            };
        bloodDonation.Groups.Add(group);
        await _context.SaveChangesAsync();
        return new AddBloodDonationToGroupResultDto()
        {
            Success = true,
            Message = "İşlem Başarıyla Tamamlandı"
        };
    }

    public async Task<IEnumerable<GetGroupBySupervisorIdResultDto>> GetGroupsBySupervisorIdAsync(Guid supervisorId)
    {
        var groups = await _context.Groups.Where(g => g.SupervisorId == supervisorId).ToListAsync();
        var result = groups.Select(g => new GetGroupBySupervisorIdResultDto()
        {
            Id = g.Id,
            SupervisorId = g.SupervisorId,
            Name = g.Name
        });
        return result;
    }

    public async Task<IEnumerable<GetGroupByUserIdResultDto>> GetGroupsByUserIdAsync(Guid userId)
    {
        var groups = await _context.Groups.Include(x=> x.Users).Where(x=>x.Users.Any(u=>u.Id == userId)).ToListAsync();
        return groups.Select(g => new GetGroupByUserIdResultDto()
        {
            Id = g.Id,
            SupervisorId = g.SupervisorId,
            Name = g.Name
        });
    }
}