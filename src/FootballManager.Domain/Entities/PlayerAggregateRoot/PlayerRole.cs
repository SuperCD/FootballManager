using FootballManager.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManager.Domain.Entities
{
    /// <summary>
    /// This is an enumeration class of positions of the players on the field.
    /// Enumeration class are an useful concept to extend enumeration with additional data or behavior. see https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/
    /// For this project we define a bare minimum of positions, we can be more precise about them
    /// </summary>
    public class PlayerRole : Enumeration
    {

        // Enumeration definition
        public static PlayerRole Goalkeeper = new PlayerRole(1, nameof(Goalkeeper), "GK");
        public static PlayerRole Defender = new PlayerRole(2, nameof(Defender), "DEF");
        public static PlayerRole Midfielder = new PlayerRole(3, nameof(Midfielder), "MID");
        public static PlayerRole Attacker = new PlayerRole(4, nameof(Attacker), "ATK");

        // Additional Enum Data
        public string Acronym { get; set; }
        // Constructior
        public PlayerRole(int id, string name, string acronym): base (id, name)
        {
            Acronym = acronym;
        }

        public static PlayerRole GetByAcronym(string roleAcronym) => GetAll<PlayerRole>().Single(x => x.Acronym == roleAcronym);
    }

    

}
