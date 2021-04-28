using System;

namespace FootballManager.API.Controllers
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FoundedIn { get; set; }
    }
}