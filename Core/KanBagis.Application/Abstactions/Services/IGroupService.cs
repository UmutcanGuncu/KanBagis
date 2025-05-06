using KanBagis.Application.DTOs;

namespace KanBagis.Application.Abstactions.Services;

public interface IGroupService
{
    public Task<AddUserToGroupResultDto> AddUserToGroupAsync(Guid groupId, Guid userId);
    public Task<AddUserToGroupResultDto> AddUserToGroupAsync(Guid groupId, Guid userId, Guid supervisorId);
    public Task<CreateGroupResultDto> CreateGroupAsync(CreateGroupDto createGroupDto);
    public Task<AddBloodDonationToGroupResultDto>  AddBloodDonationToGroupAsync(Guid bloodDonationId, Guid groupId);
    public Task<IEnumerable<GetGroupBySupervisorIdResultDto>> GetGroupBySupervisorIdAsync(Guid supervisorId);
}