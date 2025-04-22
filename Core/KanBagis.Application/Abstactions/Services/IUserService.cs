
using KanBagis.Domain.Entities;

namespace KanBagis.Application.Abstactions.Services;

public interface IUserService 
{
    Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenExpiration, int refreshTokenExpiration);

}