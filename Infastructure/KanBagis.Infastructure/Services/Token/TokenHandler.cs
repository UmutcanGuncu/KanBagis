using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using KanBagis.Application.Abstactions.Token;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KanBagis.Infastructure.Services.Token;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Application.DTOs.Token CreateAccessToken()
    {
        Application.DTOs.Token token = new();
        //Security key'in simetriğini alırız
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        //şifrelenmiş kimliği oluştur
        SigningCredentials signingCredentials = new(securityKey,SecurityAlgorithms.HmacSha256Signature);
        // Oluşturulacak token ayarlarını veririz
        token.Expiration = DateTime.UtcNow.AddDays(5); // 5 gün ömürlü olsun
        JwtSecurityToken securityToken = new(
            audience: _configuration["JWT:Audience"],
            issuer: _configuration["JWT:Issuer"],
            signingCredentials: signingCredentials,
            expires: token.Expiration,
            notBefore: DateTime.UtcNow); //üretildiği andan itibaren başlar ;
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        token.RefreshToken =  CreateRefreshToken();;
        return token;
    }

    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
    }
}