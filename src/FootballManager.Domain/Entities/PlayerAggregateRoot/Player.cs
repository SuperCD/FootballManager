﻿using FootballManager.Domain.Exceptions;
using FootballManager.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManager.Domain.Entities
{
    /// <summary>
    /// Represents a football player.
    /// </summary>
    public class Player : BaseEntity, IAggregateRoot
    {
        // Properties
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public FootType PreferredFoot { get; set; }
        public PlayerRole Role { get; set; }
        /// <summary>
        /// A list of statuses that affect the player
        /// </summary>
        public List<PlayerStatus> Statuses { get; private set; } = new List<PlayerStatus>();

        /// <summary>
        /// Is the player available to be in the team formation?
        /// </summary>
        public bool IsAvailable
        {
            get
            {
                return !Statuses.Any(x => x.DeterminesUnavailabilty);
            }
        }

        /// <summary>
        /// Applies a status to the player, if he was not already affected by that status
        /// </summary>
        /// <param name="status">The status to add to the player</param>
        /// <exception cref="PlayerStatusAlreadyAppliedException">If the player already had that status</exception>
        public void ApplyStatus(PlayerStatus status)
        {
            if (!Statuses.Contains(status))
            {
                Statuses.Add(status);
            }
            else
            {
                throw new PlayerStatusAlreadyAppliedException();
            }
        }

        /// <summary>
        /// Removes a status from the player, if he is already affected by that status
        /// </summary>
        /// <param name="status">The status to remove to the player</param>
        /// <exception cref="PlayerStatusNotPresentException">If the player was not actually affected by that status</exception>
        public void RemoveStatus(PlayerStatus status)
        {
            if (Statuses.Contains(status))
            {
                Statuses.Remove(status);
            }
            else
            {
                throw new PlayerStatusNotPresentException();
            }
        }
    }
}
