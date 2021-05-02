using System.Collections.Generic;

namespace FootballManager.API.Controllers
{
    public class ListFormationResponse
    {
        public int TeamId { get; set; }
        public List<FormationPositionDto> Positions { get; set; } = new List<FormationPositionDto>();
    }
}