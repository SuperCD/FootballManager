﻿using FootballManager.Domain.Exceptions;
using FootballManager.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManager.Domain.Entities
{
    public class Formation: BaseEntity
    {
        public string FormationType { get; set; }
        public List<FormationPostition> Postitions { get; private set; } = new List<FormationPostition>();
        
        /// <summary>
        ///  The team that is using this formation
        /// </summary>
        public Team ParentTeam { get; set; }

        /// <summary>
        ///  Adds a player to the first compatible position in the formation
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayer(Player player)
        {
            GuardAgainstIncompatiblePlayer(player);
            var slot = Postitions.FirstOrDefault(x => x.Role == player.Role && x.IsEmpty);
            if (slot != null)
            { 
                slot.CurrentPlayer = player;
            }
            else
            {
                throw new FormationSlotNotAvailableException();
            }
        }


        /// <summary>
        ///  Adds a player to a specific slot in the formation
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayer(Player player, int positionNo)
        {
            GuardAgainstIncompatiblePlayer(player);
            var slot = Postitions.Single(x => x.PositionNo == positionNo);

            if (!slot.IsEmpty) throw new FormationSlotNotAvailableException();
            if (slot.Role != player.Role) throw new FormationSlotIncompatibleException();

            slot.CurrentPlayer = player;
        }

        /// <summary>
        /// Removes a player from the formation
        /// </summary>
        /// <param name="player"></param>
        public void RemovePlayer(Player player)
        {
            var slot = Postitions.FirstOrDefault(x => x.CurrentPlayer == player);
            if (slot != null)
            {
                slot.CurrentPlayer = null;
            }
            else
            {
                throw new PlayerNotFoundException();
            }
        }

        /// <summary>
        ///  Check if a player is currently in the formation
        /// </summary>
        /// <param name="player"></param>
        /// <returns>True if the player is in the formation</returns>
        public bool HasPlayer(Player player)
        {
            return Postitions.Any(x => x.CurrentPlayer == player);
        }

        /// <summary>
        /// Checks if a player is unavailable for the formation and throws a relevant exception
        /// </summary>
        /// <param name="player"></param>
        private void GuardAgainstIncompatiblePlayer(Player player)
        {
            if (!player.IsAvailable) throw new PlayerUnavailableException();
            if (player.CurrentTeam != ParentTeam) throw new PlayerFromAnotherTeamException();
        }

        public static Formation Build(string formationType)
        {
            switch (formationType)
            {
                case "4-4-2": return Build442();
                default: throw new UnknownFormationTypeException();
            }
        }

        private static Formation Build442()
        {
            var formation = new Formation()
            {
                FormationType = "4-4-2"
            };

            int position = 0;
            formation.Postitions.Add(new FormationPostition(position++, PlayerRole.Goalkeeper));
            for (int i = 0; i < 4; i++)
            {
                formation.Postitions.Add(new FormationPostition(position++, PlayerRole.Defender));
            }
            for (int i = 0; i < 4; i++)
            {
                formation.Postitions.Add(new FormationPostition(position++, PlayerRole.Midfielder));
            }
            for (int i = 0; i < 2; i++)
            {
                formation.Postitions.Add(new FormationPostition(position++, PlayerRole.Attacker));
            }

            return formation;
        }

    }
}
