using System.Collections.Generic;

namespace FootballManager.API.Controllers
{
    public class ListTeamsResponse
    {
        public List<TeamDto> Teams { get; set; } = new List<TeamDto>();
    }
}