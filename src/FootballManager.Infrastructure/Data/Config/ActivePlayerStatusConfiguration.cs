using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManager.Infrastructure.Data.Config
{
    public class ActivePlayerStatusConfiguration : IEntityTypeConfiguration<ActivePlayerStatus>
    {
        public void Configure(EntityTypeBuilder<ActivePlayerStatus> builder)
        {
            builder
                .HasOne(bc => bc.Player)
                .WithMany(b => b.Statuses)
                .HasForeignKey(bc => bc.PlayerId);

            builder.Property(ci => ci.Status)
            .HasConversion(
                v => v.Id,
                v => Domain.SeedWork.Enumeration.GetById<PlayerStatus>(v));
        }
    }
}
