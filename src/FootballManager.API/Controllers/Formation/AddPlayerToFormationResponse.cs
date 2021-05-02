namespace FootballManager.API.Controllers
{
    public class AddPlayerToFormationResponse
    {
        public int TeamId { get; internal set; }
        public int PlayerId { get; internal set; }
        public int PositionNo { get; internal set; }
    }
}