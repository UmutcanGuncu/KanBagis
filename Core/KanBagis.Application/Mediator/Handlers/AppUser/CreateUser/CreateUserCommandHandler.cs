using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.AppUser.CreateUser;
using KanBagis.Application.Mediator.Results.AppUser.CreateUser;
using KanBagis.Application.Settings;
using KanBagis.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KanBagis.Application.Mediator.Handlers.AppUser.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.AppUser> _userManager;
    private readonly IAuthService _authService;
    private readonly IRoleService _roleService;
    private readonly IGroupService _groupService;
    public CreateUserCommandHandler(UserManager<Domain.Entities.AppUser> userManager, IAuthService authService, IRoleService roleService,  IGroupService groupService)
    {
        _userManager = userManager;
        _authService = authService;
        _roleService = roleService;
        _groupService = groupService;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        
        var result = await _userManager.CreateAsync(new Domain.Entities.AppUser()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Age = request.Age,
            UserName = request.Email,
            City = request.City,
            District = request.District,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            BloodGroup = request.BloodGroup,
            IsActive = true
        },request.Password);
        if (result.Succeeded)
        {
            Domain.Entities.AppUser user = await _userManager.FindByEmailAsync(request.Email);
            await _roleService.AssingRoleAsync(user, "User");
            Guid publicGroupId = AppGuids.PublicGroupId;
            await _groupService.AddUserToGroupAsync(publicGroupId, user.Id);
            return new CreateUserCommandResponse()
            {
                Succeeded = true,
                Message = "Kullanıcı Başarıyla Oluşturuldu.",
            };
        }
        return new CreateUserCommandResponse()
            {
                Succeeded = false,
                Message = result.Errors.FirstOrDefault()?.Description
            };
        
    }
}