namespace FootballManager.API.Requests
{
    public class CreatePlayerRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RoleAcronym { get; set; }
    }
}