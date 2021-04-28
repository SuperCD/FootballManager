namespace FootballManager.API.Controllers.Players
{
    public class UpdatePlayerRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RoleAcronym { get; set; }
    }
}