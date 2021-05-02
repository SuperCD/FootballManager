using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballManager.API.Dto
{
    /// <summary>
    /// Class that extends PlayerDto with additional details
    /// </summary>
    public class PlayerDetailsDto: PlayerDto
    {
        public List<string> Statuses { get; set; }
    }
}
