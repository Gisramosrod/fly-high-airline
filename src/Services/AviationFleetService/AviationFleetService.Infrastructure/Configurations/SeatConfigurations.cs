﻿using AviationFleetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviationFleetService.Infrastructure.Configurations
{
    internal sealed class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("Seats");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.SeatNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(s => s.SeatClass)
                .IsRequired();

            builder.Property(s => s.SeatType)
                .IsRequired();
        }
    }
}
