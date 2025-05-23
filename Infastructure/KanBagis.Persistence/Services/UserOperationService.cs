using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.DTOs;
using KanBagis.Domain.Entities;
using KanBagis.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KanBagis.Persistence.Services;

public class UserOperationService(KanBagisDbContext _context, UserManager<AppUser> _userManager, IAuthService _authService) : IUserOperationService
{
    public async Task<UserDto> GetUserInformationAsync(string userId)
    {
        Guid id = Guid.Parse(userId);
        var value = await _context.Users.FirstOrDefaultAsync(x=> x.Id == id);
        if (value == null)
            return new UserDto();
        var userDto = new UserDto()
        {
            FirstName = value.FirstName,
            LastName = value.LastName,
            Email = value.Email,
            City = value.City,
            District = value.District,
            Gender = value.Gender,
            Age = value.Age,
            BloodGroup = value.BloodGroup,
            Phone = value.PhoneNumber
        };
        return userDto;
    }

    public async Task<UpdateUserInformationResultDTO> UpdateUserInformationAsync(UpdateUserInformationDTO updateUserInformationDTO)
    {
        Guid id = Guid.Parse(updateUserInformationDTO.UserId);
        var value = await _context.Users.FirstOrDefaultAsync(x=> x.Id == id);
        if (value == null)
        {
            return new()
            {
                Success = false,
                Message = "Kullanıcı Bulunamadı",
                UserId = updateUserInformationDTO.UserId
            };
        }
        var result = await _userManager.CheckPasswordAsync(value, updateUserInformationDTO.OldPassword);
        if (result)
        {
            value.Email = updateUserInformationDTO.Email;
            value.City = updateUserInformationDTO.City;
            value.District = updateUserInformationDTO.District;
            value.PhoneNumber = updateUserInformationDTO.PhoneNumber;
            _context.Users.Update(value);
            await _context.SaveChangesAsync();
            if (updateUserInformationDTO.NewPassword != null)
            { 
                var passwordResult = await _authService.UpdatePassword(updateUserInformationDTO.OldPassword, updateUserInformationDTO.NewPassword,updateUserInformationDTO.UserId);
                if (passwordResult)
                    return new UpdateUserInformationResultDTO()
                    {
                        Success = true,
                        UserId = updateUserInformationDTO.UserId,
                        Message = "Başarıyla Bilgiler Güncellendi"
                    };
                return new()
                {
                    Success = false,
                    UserId = updateUserInformationDTO.UserId,
                    Message = "Şifre Güncellenemedi"
                };
            }
           
            return new()
            {
                Success = true,
                UserId = updateUserInformationDTO.UserId,
                Message = "Başarıyla Bilgiler Güncellendi"

            };
        }
        return new()
        {
            Success = false,
            UserId = updateUserInformationDTO.UserId,
            Message = "Mevcut Şifrenizi Hatalı Girdiniz"
        };
    }

    public async Task<UserByEmailDto> GetUserInformationByEmailAsync(string email)
    {
        var value = await _context.Users.FirstOrDefaultAsync(x=> x.Email == email);
        if (value == null)
        {
            return new();
        }

        return new()
        {
            Id = value.Id,
            FirstName = value.FirstName,
            LastName = value.LastName,
            Email = value.Email,
            City = value.City,
            District = value.District
        };
    }
}