using System.Security.Claims;
using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Abstactions.Token;
using KanBagis.Application.DTOs;
using KanBagis.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppRole = KanBagis.Domain.Entities.AppRole;

namespace KanBagis.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly IUserService _userService;
    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
        _userService = userService;
        _roleManager = roleManager;
    }

    public async Task<LoginDTO> LoginAsync(string email, string password)
    {
        Domain.Entities.AppUser user =  await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new LoginDTO()
            {
                Succeeded = false,
                Message = "Kullanıcı Adı veya Şifre Hatalı"
            };
        }
        SignInResult result =  await _signInManager.CheckPasswordSignInAsync(user,password, false);
        if (result.Succeeded)
        { 
            await _signInManager.PasswordSignInAsync(user,password,true,false);
            //yetkilendirme işlemleri yapılacak
            var token = await  _tokenHandler.CreateAccessToken(user.Id.ToString());
            await _userService.UpdateRefreshToken(token.AccessToken,user,token.Expiration,1);
            return new()
            {
                Succeeded = true,
                Message = "Başarıyla Giriş Yapılmıştır",
                Token = token
            };
        }
        return new LoginDTO()
        {
            Succeeded = false,
            Message = "Kullanıcı Adı veya Şifre Hatalı"
        };
    }

    public  async Task<LoginDTO> RefreshTokenLoginAsync(string refreshToken)
    {
        AppUser? user  = await _userManager.Users.FirstOrDefaultAsync(x=>x.RefreshToken == refreshToken);
        if (user != null && user.RefreshTokenExpiry > DateTime.UtcNow)
        {
            var token = await _tokenHandler.CreateAccessToken(user.Id.ToString());
            await _userService.UpdateRefreshToken(token.RefreshToken,user,token.Expiration,1);
            return new()
            {
                Succeeded = true,
                Message = "Giriş İşlemi Başarılı",
                Token = token
            };
        }

        return new()
        {
            Succeeded = false,
            Message = "Giriş İşlemi Başarısız",
            Token = null
        };
    }
    
}