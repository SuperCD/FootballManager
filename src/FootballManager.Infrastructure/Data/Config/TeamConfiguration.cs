using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManager.Infrastructure.Data.Config
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(ci => ci.Id)
                .UseHiLo("Team_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.FoundedIn);

            var roosterNavigation = builder.Metadata.FindNavigation(nameof(Team.Rooster));
            roosterNavigation.SetPropertyAccessMode(PropertyAccessMode.Property);

            var formationNavigation = builder.Metadata.FindNavigation(nameof(Team.Formation));
            formationNavigation.SetPropertyAccessMode(PropertyAccessMode.Property);

        }
    }
}
