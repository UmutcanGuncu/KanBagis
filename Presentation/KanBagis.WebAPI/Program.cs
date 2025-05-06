
using System.Text;
using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Abstactions.Token;
using KanBagis.Application.Mediator.Commands.Hospital;
using KanBagis.Application.Mediator.Handlers.AppUser.CreateUser;
using KanBagis.Application.Mediator.Handlers.AppUser.LoginUser;
using KanBagis.Application.Mediator.Handlers.AppUser.LogoutUser;
using KanBagis.Application.Mediator.Handlers.AppUser.RefreshTokenLogin;
using KanBagis.Application.Mediator.Handlers.BloodDonation;
using KanBagis.Application.Mediator.Handlers.City;
using KanBagis.Application.Mediator.Handlers.District;
using KanBagis.Application.Mediator.Handlers.Hospital;
using KanBagis.Application.Mediator.Queries.BloodDonation;
using KanBagis.Domain.Entities;
using KanBagis.Persistence.Contexts;
using KanBagis.Persistence.Services;
using KanBagis.WebAPI.Localizations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TokenHandler = KanBagis.Infastructure.Services.Token.TokenHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(Program).Assembly,
    typeof(CreateUserCommandHandler).Assembly ,
    typeof(LoginUserCommandHandler).Assembly ,
    typeof(LogoutUserCommandHandler).Assembly ,
    typeof(RefreshTokenCommandHandler).Assembly ,
    typeof(GetFilteredQueryHandler).Assembly,
    typeof(CreateCityCommandRequestHandler).Assembly,
    typeof(CreateDistrictCommandRequestHandler).Assembly,
    typeof(GetAllBloodDonationQueryHandler).Assembly,
    typeof(GetFilteredBloodDonationQueryHandler).Assembly,
    typeof(CreateHospitalsCommandRequestHandler).Assembly,
    typeof(CreateDistrictExcelCommandRequestHandler).Assembly
));

builder.Services.AddScoped<ITokenHandler, TokenHandler>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IHospitalService, HospitalService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<IBloodDonationService, BloodDonationService>();
builder.Services.AddScoped<IUserOperationService, UserOperationService>();
builder.Services.AddScoped<IGroupService, GroupService>();
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

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    opt.Lockout.MaxFailedAccessAttempts = 4; // 4 tane başarısız giriş denemesinden sonra 5 dk giriş yapılamayacak
})
    .AddEntityFrameworkStores<KanBagisDbContext>()
    .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
    .AddDefaultTokenProviders();
//builder.Services.AddAuthorization();
builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
    .AddJwtBearer(opt =>
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
            LifetimeValidator = ( notBefore,  expires, securityToken,  validationParameters) => expires != null ? expires> DateTime.UtcNow : false,
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
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