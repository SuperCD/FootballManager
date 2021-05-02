using Bogus;
using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
                    await catalogContext.Players.AddRangeAsync(
                        GetPreconfiguredPlayers());
                    await catalogContext.Teams.AddRangeAsync(
                        GetPreconfiguredTeams());

                    await catalogContext.SaveChangesAsync();

                    var player = await catalogContext.Players.FirstAsync();
                    var team = await catalogContext.Teams.FirstAsync();

                    team.AddPlayer(player);

                    await catalogContext.SaveChangesAsync();
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

        private static IEnumerable<Player> GetPreconfiguredPlayers()
        {
            // Create a faker to generate players
            var playerGenerator = new Faker<Player>()
                //Use an enum outside scope.
                .RuleFor(u => u.PreferredFoot, f => f.PickRandom<FootType>())
                //Basic rules using built-in generators
                .RuleFor(u => u.Name, (f, u) => f.Name.FirstName(Bogus.DataSets.Name.Gender.Male))
                .RuleFor(u => u.Surname, (f, u) => f.Name.LastName(Bogus.DataSets.Name.Gender.Male))
                .RuleFor(u => u.BirthDate, (f, u) => f.Date.Between(new DateTime(1988, 1, 1), new DateTime(2003, 1, 1)))
                .RuleFor(u => u.Role, (f, u) => Domain.SeedWork.Enumeration.GetById<PlayerRole>(f.Random.Int(1, 4)));
            return playerGenerator.Generate(20);
        }
    }
}
