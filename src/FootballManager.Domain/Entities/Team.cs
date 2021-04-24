using FootballManager.Domain.Exceptions;
using FootballManager.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballManager.Domain.Entities
{
    public class Team: BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public DateTime FoundedIn { get; set; }

        /// <summary>
        ///  The full list of player that play for this team, including the ones not in the current formation
        /// </summary>
        public List<Player> Rooster { get; private set; } = new List<Player>();
 
        /// <summary>
        /// Adds a player to the rooster
        /// </summary>
        /// <param name="joiningPlayer">A player that should not be playing for the team</param>
        public void AddPlayer(Player joiningPlayer)
        {
            if (Rooster.Contains(joiningPlayer))
            {
                throw new PlayerAlreadyInTeamException();
            }

            Rooster.Add(joiningPlayer);
            
            // Update the player join status
            joiningPlayer.CurrentTeam = this;
        }

        public void RemovePlayer(Player leavingPlayer)
        {
            if (!Rooster.Contains(leavingPlayer))
            {
                throw new PlayerNotInTeamException();
            }

            Rooster.Remove(leavingPlayer);

            leavingPlayer.CurrentTeam = null;
        }


    }
}
