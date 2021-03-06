using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace FootballManager.Infrastructure.Data.Config
{
    public class FormationPostitionConfiguration : IEntityTypeConfiguration<FormationPostition>
    {
        public void Configure(EntityTypeBuilder<FormationPostition> builder)
        {
            builder.HasKey(p => new { p.FormationId, p.PositionNo });

            builder.Property(p => p.PositionNo).ValueGeneratedNever();

            builder.Property(p => p.Role)
            .IsRequired(true)
            .HasConversion(
                v => v.Id,
                v => Domain.SeedWork.Enumeration.GetById<PlayerRole>(v));

            builder.HasOne(p => p.Player);

        }

    }
}
