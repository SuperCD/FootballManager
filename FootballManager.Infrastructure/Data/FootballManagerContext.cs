using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FootballManager.Infrastructure.Data
{
    public class FootballManagerContext : DbContext
    {
        public FootballManagerContext(DbContextOptions<FootballManagerContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatus> PlayerStatuses { get; set; }
        //public DbSet<Team> Teams { get; set; }
        //public DbSet<Formation> Formations { get; set; }
        //public DbSet<FormationPostition> FormationPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
