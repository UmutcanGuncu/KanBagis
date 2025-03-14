using System.Security.Claims;
using KanBagis.Application.Abstactions.Services;
using KanBagis.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace KanBagis.Persistence.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<Domain.Entities.AppRole> _roleManager;
    private readonly UserManager<Domain.Entities.AppUser> _userManager;

    public RoleService(UserManager<AppUser> userManager, RoleManager<Domain.Entities.AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> AssingRoleAsync(AppUser user, string roleName = "User")
    {
        if (user == null)
            return false;

        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new AppRole { Name = roleName });
            
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        if (userRoles.Contains(roleName))  // Kullanıcının zaten bu rolü var mı?
            return true;  // Zaten bu role sahipse tekrar eklemeye gerek yok.

        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded;
    }


    public async Task<IEnumerable<Claim>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaim =  roles.Select(role => new Claim(ClaimTypes.Role, role));
            return roleClaim;
        }
        return new List<Claim>();
    }

    public Task<bool> UpdateUserRolesAsync(AppUser user, IEnumerable<Claim> roles)
    {
        throw new NotImplementedException();
    }
}