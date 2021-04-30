using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManager.Infrastructure.Data.Config
{
    public class PlayerStatusConfiguration : IEntityTypeConfiguration<PlayerStatus>
    {
        public void Configure(EntityTypeBuilder<PlayerStatus> builder)
        {
            builder.Property(ci => ci.Id)
                .UseHiLo("player_status_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);
        }
    }
}
