using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.DTOs;
using KanBagis.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace KanBagis.Persistence.Services;

public class UserOperationService(KanBagisDbContext _context) : IUserOperationService
{
    public async Task<UserDto> GetUserInformation(string userId)
    {
        Guid id = Guid.Parse(userId);
        var value = await _context.Users.FirstOrDefaultAsync(x=> x.Id == id);
        var userDto = new UserDto()
        {
            FirstName = value.FirstName,
            LastName = value.LastName,
            Email = value.Email,
            City = value.City,
            District = value.District,
            Gender = value.Gender,
            BloodGroup = value.BloodGroup
        };
        return userDto;
    }
}