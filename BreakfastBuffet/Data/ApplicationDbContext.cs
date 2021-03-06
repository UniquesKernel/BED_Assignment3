using BreakfastBuffet.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffet.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ReservationModel> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ReservationModel>()
        .HasKey(r => new {r.RoomNumber, r.ReservationDate});
      base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder option)
    {
      option.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-BreakfastBuffet;Trusted_Connection=True;MultipleActiveResultSets=true");
    }
  }
}