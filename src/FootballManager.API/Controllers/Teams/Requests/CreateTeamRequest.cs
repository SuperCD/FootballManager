using System;

namespace FootballManager.API.Controllers
{
    public class CreateTeamRequest
    {
        public string Name { get; set; }
        public DateTime FoundedIn { get; set; }
    }
}