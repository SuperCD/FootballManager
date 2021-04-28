using System;

namespace FootballManager.API.Controllers
{
    public class UpdateTeamRequest
    {
        public string Name { get; set; }
        public DateTime FoundedIn { get; set; }
    }
}