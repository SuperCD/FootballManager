using FootballManager.Domain.Entities;
using FootballManager.Domain.Exceptions;
using NUnit.Framework;
using System;
using System.Linq;

namespace FootballManager.Domain.UnitTest.Entities
{
    public class PlayerTests
    {
        Player playerUnderTest;
        
        [SetUp]
        public void Setup()
        {
            playerUnderTest = new Player();
            playerUnderTest.Name = "Giacomo";
            playerUnderTest.Surname = "Pellegrini";
            playerUnderTest.PreferredFoot = FootType.Right;
            playerUnderTest.Role = PlayerRole.Midfielder;
        }

        [Test]
        public void PlayerIsAvailableOnCreation()
        {
            Assert.True(playerUnderTest.IsAvailable);
        }

        [Test]
        public void ApplyStatusAddsStatusToThePlayer()
        {
            Assert.IsEmpty(playerUnderTest.Statuses);
            playerUnderTest.ApplyStatus(PlayerStatus.Disqualified);
            Assert.AreEqual(playerUnderTest.Statuses.Count, 1);
            Assert.That(playerUnderTest.Statuses.Any(x=>x.Status == PlayerStatus.Disqualified));
        }

        [Test]
        public void RemoveStatusRemovesStatusFromThePlayer()
        {
            Assert.IsEmpty(playerUnderTest.Statuses);
            playerUnderTest.ApplyStatus(PlayerStatus.Disqualified);
            playerUnderTest.RemoveStatus(PlayerStatus.Disqualified);
            Assert.IsEmpty(playerUnderTest.Statuses);
        }

        [Test]
        public void PlayerIsNotAvailableIfDisqualified()
        {
            Assert.True(playerUnderTest.IsAvailable);
            playerUnderTest.ApplyStatus(PlayerStatus.Disqualified);
            Assert.False(playerUnderTest.IsAvailable);
        }

        [Test]
        public void PlayerThrowsIfAStatusIsAppliedTwice()
        {
            playerUnderTest.ApplyStatus(PlayerStatus.Disqualified);
            Assert.Throws<PlayerStatusAlreadyAppliedException>(() => playerUnderTest.ApplyStatus(PlayerStatus.Disqualified));
        }

        [Test]
        public void PlayerThrowsIfANonPresentStatusIsRemoved()
        {
            Assert.Throws<PlayerStatusNotPresentException>(() => playerUnderTest.RemoveStatus(PlayerStatus.Injured));
        }

        [Test]
        public void PlayerIsFreeAgentUponCreation()
        {
            Assert.True(playerUnderTest.IsFreeAgent);
        }

        [Test]
        public void PlayerIsNotFreeAgentIfJoinsTeam()
        {
            playerUnderTest.AssignToTeam(new Team()
            {
                Name = "Test Team",
                FoundedIn = DateTime.Now
            });

            Assert.False(playerUnderTest.IsFreeAgent);
        }

    }
}