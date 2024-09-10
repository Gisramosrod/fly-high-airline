using FlightService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightService.Infrastructure.Configurations {

    internal sealed class AirportConfigurations : IEntityTypeConfiguration<Airport> {

        public void Configure(EntityTypeBuilder<Airport> builder) {

            builder.ToTable("Airports");  

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
