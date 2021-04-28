using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballManager.Infrastructure.Data
{
    public class FootballManagerContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<FormationPostition> FormationPositions { get; set; }
    }
}
