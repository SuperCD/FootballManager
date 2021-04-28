using System.Collections.Generic;

namespace FootballManager.API.Controllers.Players
{
    public class ListPlayersResponse
    {
        public List<PlayerDto> Players { get; set; } = new List<PlayerDto>();
    }
}