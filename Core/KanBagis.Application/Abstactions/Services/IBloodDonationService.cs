using KanBagis.Application.DTOs;
using KanBagis.Application.DTOs.BloodDonation.Response;
using KanBagis.Domain.Entities;

namespace KanBagis.Application.Abstactions.Services;

public interface IBloodDonationService
{
    public Task<AddBloodDonationDTO> CreateAsync(BloodDonationDTO bloodDonationDto);
    public Task<IEnumerable<GetAllBloodDonationResponseDTO>> GetPublicAllAsync();
    public Task<IEnumerable<GetUserIdBloodDonationResponseDTO>> GetByUserIdAsync(Guid userId);
    public Task<IEnumerable<GetFilteredBloodDonationResponseDTO>> GetPublicFilteredAsync(string city = null, string district= null, string hospitalName = null);
    public Task<bool> ChangeBloodDonationStatusAdminAsync(Guid bloodDonationId, bool status);
    public Task<IEnumerable<GetBloodDonationsByUserGroupsResponseDto>> GetBloodDonationsByUserGroupsAsync(Guid userId);
    public Task<UpdateBloodDonationResultDto> UpdateBloodDonationAsync(UpdateBloodDonationDto bloodDonationDto);
}