namespace BESL.Domain.Entities
{
    public class PlayerTeam
    {
        public string PlayerId { get; set; }
        public Player Player { get; set; }

        public string TeamId { get; set; }
        public Team Team { get; set; }
    }
}
