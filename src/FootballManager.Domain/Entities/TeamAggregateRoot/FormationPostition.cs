using FootballManager.Domain.SeedWork;

namespace FootballManager.Domain.Entities
{
    public class FormationPostition
    {

        public int PositionNo { get; private set; }
        public PlayerRole Role { get; private set; }
        public Player Player { get; set; }
        /// <summary>
        /// Is this position empty?
        /// </summary>
        public bool IsEmpty => Player == null;
        
        // Navigation
        public int FormationId { get; private set; }
        public Formation Formation { get; private set; }

        public FormationPostition(int positionNo, PlayerRole role)
        {
            PositionNo = positionNo;
            Role = role;
        }

        // Required by EF
        private FormationPostition()
        {

        }
    }
}