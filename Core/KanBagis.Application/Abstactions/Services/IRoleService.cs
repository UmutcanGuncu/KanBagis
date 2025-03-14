using System.Security.Claims;
using KanBagis.Domain.Entities;

namespace KanBagis.Application.Abstactions.Services;

public interface IRoleService
{
    Task<bool> AssingRoleAsync(AppUser user , string roleName);
    Task<IEnumerable<Claim>> GetUserRolesAsync(string userId);
    Task<bool> UpdateUserRolesAsync(AppUser user, IEnumerable<Claim> roles);
}