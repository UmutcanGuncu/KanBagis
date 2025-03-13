using KanBagis.Domain.Entities;
using KanBagis.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KanBagis.Persistence.Contexts;

public class KanBagisDbContext :IdentityDbContext<AppUser, AppRole, Guid>
{
    public KanBagisDbContext(DbContextOptions<KanBagisDbContext> options) : base(options)
    {
        
    }
    public DbSet<City> Cities { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Hospital> Hospitals { get; set; }
    public DbSet<BloodDonation> BloodDonations { get; set; }
}