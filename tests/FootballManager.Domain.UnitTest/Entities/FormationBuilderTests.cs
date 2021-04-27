using FootballManager.Domain.Entities;
using FootballManager.Domain.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManager.Domain.UnitTest.Entities
{
    public class FormationBuilderTests
    {
        [Test]
        public void Formation442Has11Players()
        {
            var formation = Formation.Build("4-4-2");
            Assert.AreEqual(formation.Postitions.Count, 11);
        }

        [Test]
        public void Formation442Has1Goalkeeper()
        {
            var formation = Formation.Build("4-4-2");
            Assert.AreEqual(formation.Postitions.Count(x => x.Role == PlayerRole.Goalkeeper), 1);
        }

        [Test]
        public void Formation442Has4Defenders()
        {
            var formation = Formation.Build("4-4-2");
            Assert.AreEqual(formation.Postitions.Count(x=>x.Role == PlayerRole.Defender) , 4);
        }

        [Test]
        public void Formation442Has4Midfielders()
        {
            var formation = Formation.Build("4-4-2");
            Assert.AreEqual(formation.Postitions.Count(x => x.Role == PlayerRole.Midfielder), 4);
        }

        [Test]
        public void Formation442Has2Attackers()
        {
            var formation = Formation.Build("4-4-2");
            Assert.AreEqual(formation.Postitions.Count(x => x.Role == PlayerRole.Attacker), 2);
        }

        [Test]
        public void FormationBuilderThrowsIfFormationIsUnknown()
        {
            Assert.Throws<UnknownFormationTypeException>(() => Formation.Build("4-4-3"));
            
        }
    }
}
