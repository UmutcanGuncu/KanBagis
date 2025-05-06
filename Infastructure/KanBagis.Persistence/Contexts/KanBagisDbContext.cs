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
    public DbSet<Group> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Group>().HasData(
            new Group
            {
                Id = Guid.Parse("01021fcf-ac13-4437-9996-205c3708f34e"),
                Name = "Public",
                CreatedDate = DateTime.UtcNow
            });
    }
}