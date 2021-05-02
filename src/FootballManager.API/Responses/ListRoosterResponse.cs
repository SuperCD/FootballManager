using FootballManager.API.Dto;
using System.Collections.Generic;

namespace FootballManager.API.Responses
{
    public class ListRoosterResponse
    {
        public int TeamId { get; set; }
        public List<PlayerDto> Players { get; set; } = new List<PlayerDto>();
    }
}