using FootballManager.API.Controllers.Players;
using System.Collections.Generic;

namespace FootballManager.API.Controllers
{
    public class ListRoosterResponse
    {
        public int TeamId { get; set; }
        public List<PlayerDto> Players { get; set; } = new List<PlayerDto>();
    }
}