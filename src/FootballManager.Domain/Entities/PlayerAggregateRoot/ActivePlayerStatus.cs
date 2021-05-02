using FootballManager.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballManager.Domain.Entities
{
    /// <summary>
    /// Used to create a many to many relationship between statuses and players
    /// </summary>
    public class ActivePlayerStatus: BaseEntity
    {
        private ActivePlayerStatus()
        {

        }

        public ActivePlayerStatus(Player player, PlayerStatus status)
        {
            Player = player;
            Status = status;
        }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public PlayerStatus Status { get; set; }
    }
}
