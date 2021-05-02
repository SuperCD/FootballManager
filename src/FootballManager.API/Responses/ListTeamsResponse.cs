using FootballManager.API.Dto;
using System.Collections.Generic;

namespace FootballManager.API.Responses
{
    public class ListTeamsResponse
    {
        public List<TeamDto> Teams { get; set; } = new List<TeamDto>();
    }
}