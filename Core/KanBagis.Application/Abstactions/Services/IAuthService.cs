namespace KanBagis.Application.Abstactions.Services;

public interface IAuthService
{
    Task<DTOs.LoginDTO> LoginAsync(string email, string password);
    Task<DTOs.LoginDTO> RefreshTokenLoginAsync(string refreshToken);
}