using System;

namespace FootballManager.API.Requests
{
    public class UpdateTeamRequest
    {
        public string Name { get; set; }
        public DateTime FoundedIn { get; set; }
    }
}