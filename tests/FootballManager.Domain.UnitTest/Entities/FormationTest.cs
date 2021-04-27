using Bogus;
using FootballManager.Domain.Entities;
using FootballManager.Domain.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManager.Domain.UnitTest.Entities
{
    public class FormationTests
    {
        Team teamUnderTest;
        private Faker<Player> playerGenerator;
        private int playerIds;

        [SetUp]
        public void Setup()
        {
            teamUnderTest = new Team()
            {
                Name = "Test Team",
                FoundedIn = DateTime.Now
            };

            playerIds = 1;
            // Create a faker to generate players
            playerGenerator = new Faker<Player>()
                .RuleFor(o => o.Id, f => playerIds++)
                //Use an enum outside scope.
                .RuleFor(u => u.PreferredFoot, f => f.PickRandom<FootType>())
                //Basic rules using built-in generators
                .RuleFor(u => u.Name, (f, u) => f.Name.FirstName(Bogus.DataSets.Name.Gender.Male))
                .RuleFor(u => u.Surname, (f, u) => f.Name.LastName(Bogus.DataSets.Name.Gender.Male))
                .RuleFor(u => u.BirthDate, (f, u) => f.Date.Between(new DateTime(1988, 1, 1), new DateTime(2003, 1, 1)));
        }

        [Test]
        public void AddToFormationToCompatibleSlotWithSlotAvailable()
        {
            // Generate two defender
            var defender1 = playerGenerator.Generate();
            defender1.Role = PlayerRole.Defender;
            var defender2 = playerGenerator.Generate();
            defender2.Role = PlayerRole.Defender;

            // Add them to the rooster
            teamUnderTest.AddPlayer(defender1);
            teamUnderTest.AddPlayer(defender2);

            // Add them to the formation
            teamUnderTest.Formation.AddPlayer(defender1);
            Assert.That(teamUnderTest.Formation.Postitions.Any(x => x.CurrentPlayer == defender1));
            teamUnderTest.Formation.AddPlayer(defender2);
            Assert.That(teamUnderTest.Formation.Postitions.Any(x => x.CurrentPlayer == defender2));
        }

        [Test]
        public void AddToFormationToCompatibleSlotWithNoSlotAvailable()
        {
            // Setup the test by filling all the defending roles
            for (int i = 0; i < 4; i ++)
            { 
                var player = playerGenerator.Generate();
                player.Role = PlayerRole.Defender;
                teamUnderTest.AddPlayer(player);
                teamUnderTest.Formation.AddPlayer(player);
            }

            // Add an extra defender
            var extraDefender = playerGenerator.Generate();
            extraDefender.Role = PlayerRole.Defender;
            teamUnderTest.AddPlayer(extraDefender);
            Assert.Throws<FormationSlotNotAvailableException>(() => teamUnderTest.Formation.AddPlayer(extraDefender));
        }

        [Test]
        public void AddToFormationToSpecificSlotWithSlotAvailable()
        {
            // Generate two defender
            var defender1 = playerGenerator.Generate();
            defender1.Role = PlayerRole.Defender;

            // Add them to the rooster
            teamUnderTest.AddPlayer(defender1);

            // Add them to the formation
            teamUnderTest.Formation.AddPlayer(defender1, 2);
            Assert.That(teamUnderTest.Formation.Postitions.Any(x => (x.PositionNo == 2) && (x.CurrentPlayer == defender1)));
        }

        [Test]
        public void AddToFormationToSpecificSlotInWrongSlotThrows()
        {
            var attacker1 = playerGenerator.Generate();
            attacker1.Role = PlayerRole.Attacker;

            // Add them to the rooster
            teamUnderTest.AddPlayer(attacker1);

            // Add them to the formation
            Assert.Throws<FormationSlotIncompatibleException>(() => teamUnderTest.Formation.AddPlayer(attacker1, 2));
        }

        [Test]
        public void AddToFormationToSpecificSlotInOccupiedSlotThrows()
        {
            var player = playerGenerator.Generate();
            player.Role = PlayerRole.Defender;

            // Add them to the rooster
            teamUnderTest.AddPlayer(player);

            // Add them to the formation
            teamUnderTest.Formation.AddPlayer(player, 2);

            var player2 = playerGenerator.Generate();
            player2.Role = PlayerRole.Defender;

            // Add them to the rooster
            teamUnderTest.AddPlayer(player2);

            // Add them to the formation
            Assert.Throws<FormationSlotNotAvailableException>(() => teamUnderTest.Formation.AddPlayer(player2, 2));
        }

        [Test]
        public void RemoveFromTeamAlsoRemovesFromFormation()
        {
            var player = playerGenerator.Generate();
            player.Role = PlayerRole.Defender;

            // Add them to the rooster
            teamUnderTest.AddPlayer(player);

            teamUnderTest.Formation.AddPlayer(player);
            Assert.That(teamUnderTest.Formation.Postitions.Any(x => x.CurrentPlayer == player));

            teamUnderTest.RemovePlayer(player);

            Assert.That(!teamUnderTest.Formation.Postitions.Any(x => x.CurrentPlayer == player));
        }

        [Test]
        public void RemoveFromFormation()
        {
            var player = playerGenerator.Generate();
            player.Role = PlayerRole.Defender;

            // Add them to the rooster
            teamUnderTest.AddPlayer(player);

            teamUnderTest.Formation.AddPlayer(player);
            Assert.That(teamUnderTest.Formation.Postitions.Any(x =>x.CurrentPlayer == player));

            teamUnderTest.Formation.RemovePlayer(player);
            Assert.That(!teamUnderTest.Formation.Postitions.Any(x => x.CurrentPlayer == player));
        }

        [Test]
        public void RemoveFromFormationUnslottedPlayerThrows()
        {
            var player = playerGenerator.Generate();
            player.Role = PlayerRole.Defender;

            // Add them to the rooster
            teamUnderTest.AddPlayer(player);

            Assert.Throws<PlayerNotFoundException>(() => teamUnderTest.Formation.RemovePlayer(player));
        }


    }
}
