using FlightService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightService.Infrastructure.Configurations
{
    internal sealed class FlightConfigurations : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.ToTable("Flights");

            builder.HasKey(f => f.Id);

            builder.ComplexProperty(f => f.Number);

            builder.Property(f => f.Status).IsRequired();

            builder.Property(f => f.Duration).IsRequired();

            builder.HasOne(f => f.Plane)
                .WithMany()
                .HasForeignKey(f => f.PlaneId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.DepartureDetail)
                .WithOne()
                .HasForeignKey<Flight>(f => f.DepartureDetailId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.ArrivalDetail)
                .WithOne()
                .HasForeignKey<Flight>(f => f.ArrivalDetailId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
