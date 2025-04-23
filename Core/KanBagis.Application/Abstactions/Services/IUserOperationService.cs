using KanBagis.Application.DTOs;

namespace KanBagis.Application.Abstactions.Services;

public interface IUserOperationService
{
    Task<UserDto> GetUserInformation(string userId);
    Task<UpdateUserInformationResultDTO> UpdateUserInformation(UpdateUserInformationDTO updateUserInformationDTO);
}