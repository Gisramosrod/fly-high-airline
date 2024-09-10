using AviationFleetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AviationFleetService.Infrastructure.Common
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Plane> Planes => Set<Plane>();
        public DbSet<PlaneService> PlaneServices => Set<PlaneService>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<Airport> Airports => Set<Airport>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
