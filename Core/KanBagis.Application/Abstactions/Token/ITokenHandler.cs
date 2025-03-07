namespace KanBagis.Application.Abstactions.Token;

public interface ITokenHandler
{
    DTOs.Token CreateAccessToken(); 
    string CreateRefreshToken();
}