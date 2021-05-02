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
        ///  The current formation on the field for the team
        /// </summary>
        public Formation Formation { get; private set; }

        public Team()
        {
            Formation = Formation.Build("4-4-2");
            Formation.ParentTeam = this;
        }

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
            joiningPlayer.AssignToTeam(this);
        }

        /// <summary>
        /// Removes a player from the rooster, and from the formation if he was in it
        /// </summary>
        /// <param name="leavingPlayer">The player leaving the team</param>
        public void RemovePlayer(Player leavingPlayer)
        {
            if (!Rooster.Contains(leavingPlayer))
            {
                throw new PlayerNotFoundException();
            }


            if (Formation.HasPlayer(leavingPlayer))
            {
                Formation.RemovePlayer(leavingPlayer);
            }

            Rooster.Remove(leavingPlayer);
            leavingPlayer.RemoveFromTeam();
        }



    }
}
