using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManager.Infrastructure.Data.Config
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.Property(ci => ci.Id)
                .UseHiLo("player_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Surname)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.BirthDate);

            builder.Property(ci => ci.PreferredFoot);

            builder.Property(ci => ci.Role)
                .IsRequired(true)
                .HasConversion(
                    v => v.Id,
                    v => Domain.SeedWork.Enumeration.GetById<PlayerRole>(v));

            var navigation = builder.Metadata.FindNavigation(nameof(Player.Statuses));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Property);

            var teamnavigation = builder.Metadata.FindNavigation(nameof(Player.CurrentTeam));
            teamnavigation.SetPropertyAccessMode(PropertyAccessMode.Property);

        }
    }
}
