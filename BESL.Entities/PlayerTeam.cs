namespace BESL.Entities
{
    using BESL.Entities.Infrastructure;

    public class PlayerTeam : BaseDeletableModel<string>
    {
        public string PlayerId { get; set; }
        public Player Player { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
