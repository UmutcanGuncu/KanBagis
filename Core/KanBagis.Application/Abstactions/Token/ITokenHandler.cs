namespace KanBagis.Application.Abstactions.Token;

public interface ITokenHandler
{
    Task<DTOs.Token> CreateAccessToken(string userId); 
    string CreateRefreshToken();
}