
using System.Text;
using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Abstactions.Token;
using KanBagis.Application.Mediator.Handlers.AppUser.CreateUser;
using KanBagis.Application.Mediator.Handlers.AppUser.LoginUser;
using KanBagis.Application.Mediator.Handlers.AppUser.LogoutUser;
using KanBagis.Application.Mediator.Handlers.AppUser.RefreshTokenLogin;
using KanBagis.Domain.Entities;
using KanBagis.Persistence.Contexts;
using KanBagis.Persistence.Services;
using KanBagis.WebAPI.Localizations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TokenHandler = KanBagis.Infastructure.Services.Token.TokenHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(Program).Assembly,
    typeof(CreateUserCommandHandler).Assembly ,
    typeof(LoginUserCommandHandler).Assembly ,
    typeof(LogoutUserCommandHandler).Assembly ,
    typeof(RefreshTokenCommandHandler).Assembly 
));

builder.Services.AddScoped<ITokenHandler, TokenHandler>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<KanBagisDbContext>(cfg =>
{
    cfg.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddCors(options =>
    options.AddPolicy("CORSPolicy", opt =>
        opt.AllowAnyOrigin() // Belirli bir origin
            .AllowAnyHeader()
            .AllowAnyMethod()
    )); // Kimlik bilgilerini destekler

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    opt.Lockout.MaxFailedAccessAttempts = 4; // 4 tane başarısız giriş denemesinden sonra 5 dk giriş yapılamayacak
})
    .AddEntityFrameworkStores<KanBagisDbContext>()
    .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            //Gelen Tokenda bakılacak parametreleri bildirecez
            ValidateAudience = true, // Oluşturacak token değerini kimlerin kullanacağını belirtiriz localhost:2025
            ValidateIssuer = true, // oluşturulacak token değerini kiin dağıttığını ifade eder
            ValidateLifetime = true, // oluşturulan token değerinin süresini kontrol etmemizi sağlar
            ValidateIssuerSigningKey = true, // üretilecek token değerinin uygulamamıza ait değer olan signinkey'in doğrulanmasıdır
            
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            LifetimeValidator = ( notBefore,  expires, securityToken,  validationParameters) => expires != null ? expires> DateTime.UtcNow : false
        };
    });    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseCors("CORSPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();