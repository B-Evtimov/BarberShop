using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Barber> Barbers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<EmailCode> EmailCodes { get; set; }
}