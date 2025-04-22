using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.DTOs;
using KanBagis.Domain.Entities;
using KanBagis.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KanBagis.Persistence.Services;

public class UserService(UserManager<AppUser> _userManager) : IUserService
{
    public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenExpiration, int refreshTokenExpiration)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = accessTokenExpiration.AddDays(refreshTokenExpiration);
        await _userManager.UpdateAsync(user);
    }

    
}