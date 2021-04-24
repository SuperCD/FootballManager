using FootballManager.Domain.Entities;
using FootballManager.Domain.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballManager.Domain.UnitTest.Entities
{
    public class TeamTests
    {
        Team teamUnderTest;

        [SetUp]
        public void Setup()
        {
            teamUnderTest = new Team()
            {
                Name = "Test Team",
                FoundedIn = DateTime.Now
            };
        }

        [Test]
        public void TeamThrowsIfPlayerJoinsTwice()
        {
            var player = new Player()
            {
                Name = "Test Player"
            };

            teamUnderTest.AddPlayer(player);
            Assert.Throws<PlayerAlreadyInTeamException>(() => teamUnderTest.AddPlayer(player));
        }

        [Test]
        public void TeamRoosterIncreasesWhenPlayerJoins()
        {
            Assert.AreEqual(teamUnderTest.Rooster.Count, 0);
            var player = new Player()
            {
                Name = "Test Player"
            };
            teamUnderTest.AddPlayer(player);
            Assert.AreEqual(teamUnderTest.Rooster.Count, 1);
        }

        [Test]
        public void TeamRoosterDecreasesWhenPlayerLeaves()
        {
            
            var player = new Player()
            {
                Name = "Test Player"
            };
            teamUnderTest.AddPlayer(player);
            Assert.AreEqual(teamUnderTest.Rooster.Count, 1);
            teamUnderTest.RemovePlayer(player);
            Assert.AreEqual(teamUnderTest.Rooster.Count, 0);
        }

        [Test]
        public void PlayerCurrentTeamChangesWhenHeJoins()
        {
            var player = new Player()
            {
                Name = "Test Player"
            };
            Assert.Null(player.CurrentTeam);
            teamUnderTest.AddPlayer(player);
            Assert.AreEqual(player.CurrentTeam, teamUnderTest);
        }
        [Test]
        public void PlayerBecomesFreeAgentWhenHeLeaves()
        {
            var player = new Player()
            {
                Name = "Test Player"
            };
            teamUnderTest.AddPlayer(player);
            Assert.False(player.IsFreeAgent);
            teamUnderTest.RemovePlayer(player);
            Assert.True(player.IsFreeAgent);
        }
    }
}
