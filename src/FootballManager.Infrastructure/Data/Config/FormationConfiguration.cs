using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManager.Infrastructure.Data.Config
{
    public class FormationConfiguration : IEntityTypeConfiguration<Formation>
    {
        public void Configure(EntityTypeBuilder<Formation> builder)
        {
            builder.Property(ci => ci.Id)
                .UseHiLo("Formation_hilo")
                .IsRequired();

            builder.Property(ci => ci.FormationType)
            .IsRequired(true)
            .HasMaxLength(10);

            builder.HasOne(ci => ci.ParentTeam);
            var teamNavigation = builder.Metadata.FindNavigation(nameof(Formation.ParentTeam));
            teamNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            var posNavigation = builder.Metadata.FindNavigation(nameof(Formation.Postitions));
            posNavigation.SetPropertyAccessMode(PropertyAccessMode.Property);

        }
    }
}
