using KanBagis.Application.DTOs;
using KanBagis.Application.DTOs.BloodDonation.Response;
using KanBagis.Domain.Entities;

namespace KanBagis.Application.Abstactions.Services;

public interface IBloodDonationService
{
    public Task<AddBloodDonationDTO> CreateAsync(BloodDonationDTO bloodDonationDto);
    public Task<IEnumerable<GetAllBloodDonationResponseDTO>> GetAllAsync();
    public Task<IEnumerable<GetUserIdBloodDonationResponseDTO>> GetByUserIdAsync(Guid userId);
    public Task<IEnumerable<GetFilteredBloodDonationResponseDTO>> GetFiltederAsync(string city = null, string district= null, string hospitalName = null);
}