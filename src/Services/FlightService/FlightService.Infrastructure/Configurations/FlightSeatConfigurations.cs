using FlightService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightService.Infrastructure.Configurations
{
    internal class FlightSeatConfigurations : IEntityTypeConfiguration<FlightSeat>
    {
        public void Configure(EntityTypeBuilder<FlightSeat> builder)
        {
            builder.ToTable("FlightSeats");

            builder.HasBaseType<Seat>();

            builder.Property(fs => fs.Available).IsRequired();
        }
    }
}
