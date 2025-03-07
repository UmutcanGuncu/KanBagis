using Microsoft.AspNetCore.Identity;

namespace KanBagis.Domain.Entities;

public class AppRole : IdentityRole<Guid>
{
    public string RoleName { get; set; }
}