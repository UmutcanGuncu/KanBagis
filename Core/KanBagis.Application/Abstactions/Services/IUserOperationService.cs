using KanBagis.Application.DTOs;

namespace KanBagis.Application.Abstactions.Services;

public interface IUserOperationService
{
    Task<UserDto> GetUserInformationAsync(string userId);
    Task<UpdateUserInformationResultDTO> UpdateUserInformationAsync(UpdateUserInformationDTO updateUserInformationDTO);
    Task<UserByEmailDto> GetUserInformationByEmailAsync(string email);
}