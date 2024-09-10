using FlightService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightService.Infrastructure.Common
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Flight> Flights => Set<Flight>();
        public DbSet<Plane> Planes => Set<Plane>();
        public DbSet<PlaneService> PlaneServices => Set<PlaneService>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<FlightDetail> FlightDetails => Set<FlightDetail>();
        public DbSet<Airport> Airports => Set<Airport>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
