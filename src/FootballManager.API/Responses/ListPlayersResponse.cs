using FootballManager.API.Dto;
using System.Collections.Generic;

namespace FootballManager.API.Responses
{
    public class ListPlayersResponse
    {
        public List<PlayerDto> Players { get; set; } = new List<PlayerDto>();
    }
}