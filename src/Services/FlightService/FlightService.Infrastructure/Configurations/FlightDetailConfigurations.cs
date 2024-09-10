using FlightService.Domain.Entities;
using FlightService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightService.Infrastructure.Configurations {

    internal class FlightDetailConfigurations : IEntityTypeConfiguration<FlightDetail> {

        public void Configure(EntityTypeBuilder<FlightDetail> builder) {

            builder.ToTable("FlightDetails");

            builder.HasKey(fd => fd.Id);

            builder.Property(fd => fd.Type)
                .HasConversion(
                x => x.ToString(),
                x => (FlightDetailType)Enum.Parse(typeof(FlightDetailType), x));

            builder.Property(fd => fd.Type).IsRequired();

            builder.Property(fd => fd.ScheduleDate).IsRequired();

            builder.Property(fd => fd.ActualDate).IsRequired(false);
        }
    }
}
