using KanBagis.Application.DTOs;

namespace KanBagis.Application.Abstactions.Services;

public interface IGroupService
{
    public Task<AddUserToGroupResultDto> AddUserToGroupAsync(Guid groupId, Guid userId);
    public Task<AddUserToGroupResultDto> AddUserToGroupAsync(Guid groupId, string email, Guid supervisorId);
    public Task<CreateGroupResultDto> CreateGroupAsync(CreateGroupDto createGroupDto);
    public Task<AddBloodDonationToGroupResultDto>  AddBloodDonationToGroupAsync(Guid bloodDonationId, Guid groupId);
    public Task<AddBloodDonationToGroupResultDto>  AddBloodDonationToGroupAsync(Guid bloodDonationId);
    public Task<IEnumerable<GetGroupBySupervisorIdResultDto>> GetGroupsBySupervisorIdAsync(Guid supervisorId);
    public Task<IEnumerable<GetGroupByUserIdResultDto>> GetGroupsByUserIdAsync(Guid userId);
}