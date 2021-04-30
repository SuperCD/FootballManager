using FootballManager.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballManager.Domain.Entities
{
    /// <summary>
    /// This is an enumeration class of possible statuses that are attached to a player
    /// Enumeration class are an useful concept to extend enumeration with additional data or behavior. see https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/
    /// There is no "available" status, but a player is available is he has no status that makes him unavailable
    /// </summary>
    public class PlayerStatus : Enumeration
    {

        // Enumeration definition
        public static PlayerStatus Injured = new PlayerStatus(1, nameof(Injured), unavailability: true);
        public static PlayerStatus Disqualified = new PlayerStatus(2, nameof(Disqualified), unavailability: true);

        public bool DeterminesUnavailabilty { get; private set; }

        // Constructior
        public PlayerStatus(int id, string name, bool unavailability): base (id, name)
        {
            DeterminesUnavailabilty = unavailability;
        }

        // Required by EF
        private PlayerStatus(): base(0, string.Empty)
        {

        }
    }

}
