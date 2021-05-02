using System;

namespace FootballManager.API.Requests
{
    public class CreateTeamRequest
    {
        public string Name { get; set; }
        public DateTime FoundedIn { get; set; }
    }
}