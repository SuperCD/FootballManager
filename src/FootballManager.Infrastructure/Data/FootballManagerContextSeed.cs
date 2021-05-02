using Bogus;
using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballManager.Infrastructure.Data
{
    public class FootballManagerContextSeed
    {
        public static async Task SeedAsync(FootballManagerContext catalogContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // catalogContext.Database.Migrate();
                if (!await catalogContext.Players.AnyAsync())
                {
                    var teams = GetPreconfiguredTeams();
                    foreach (var team in teams)
                    { 
                        catalogContext.Teams.Add(team);
                        var players = GeneratePreconfiguredTeamRooster();
                        catalogContext.Players.AddRange(players);
                        await catalogContext.SaveChangesAsync();

                        // Add the players to the team
                        foreach (var player in players)
                        {
                            team.AddPlayer(player);
                        }
                        await catalogContext.SaveChangesAsync();

                        // Add players to the formation
                        team.Formation.AddPlayer(players.First(x => x.Role == PlayerRole.Goalkeeper));

                        foreach (var defender in players.Where(x => x.Role == PlayerRole.Defender).Take(4))
                        {
                            team.Formation.AddPlayer(defender);
                        }

                        foreach (var midfielder in players.Where(x => x.Role == PlayerRole.Midfielder).Take(4))
                        {
                            team.Formation.AddPlayer(midfielder);
                        }

                        foreach (var attacker in players.Where(x => x.Role == PlayerRole.Attacker).Take(2))
                        {
                            team.Formation.AddPlayer(attacker);
                        }

                        await catalogContext.SaveChangesAsync();

                    }
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<FootballManagerContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(catalogContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Team> GetPreconfiguredTeams()
        {
            yield return new Team() { Name = "Real Madrid", FoundedIn = DateTime.Parse("1903-03-01") };
            yield return new Team() { Name = "AC Milan", FoundedIn = DateTime.Parse("1903-03-01") };
            yield return new Team() { Name = "Juventus FC", FoundedIn = DateTime.Parse("1903-03-01") };
            yield return new Team() { Name = "FK Partizan Belgrado", FoundedIn = DateTime.Parse("1903-03-01") };
        }

        private static IEnumerable<Player> GeneratePreconfiguredTeamRooster()
        {
            // Create a faker to generate players
            var playerGenerator = new Faker<Player>()
                //Use an enum outside scope.
                .RuleFor(u => u.PreferredFoot, f => f.PickRandom<FootType>())
                //Basic rules using built-in generators
                .RuleFor(u => u.Name, (f, u) => f.Name.FirstName(Bogus.DataSets.Name.Gender.Male))
                .RuleFor(u => u.Surname, (f, u) => f.Name.LastName(Bogus.DataSets.Name.Gender.Male))
                .RuleFor(u => u.BirthDate, (f, u) => f.Date.Between(new DateTime(1988, 1, 1), new DateTime(2003, 1, 1)));


            var rooster = new List<Player>();

            playerGenerator.RuleFor(u => u.Role, (f, u) => PlayerRole.Goalkeeper);
            rooster.AddRange(playerGenerator.Generate(3));

            playerGenerator.RuleFor(u => u.Role, (f, u) => PlayerRole.Defender);
            rooster.AddRange(playerGenerator.Generate(10));

            playerGenerator.RuleFor(u => u.Role, (f, u) => PlayerRole.Midfielder);
            rooster.AddRange(playerGenerator.Generate(10));

            playerGenerator.RuleFor(u => u.Role, (f, u) => PlayerRole.Attacker);
            rooster.AddRange(playerGenerator.Generate(7));

            return rooster;

        }
    }
}
