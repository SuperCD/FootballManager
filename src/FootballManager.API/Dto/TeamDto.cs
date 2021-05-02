using System;

namespace FootballManager.API.Dto
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FoundedIn { get; set; }
    }
}