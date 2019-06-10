namespace BESL.Domain.Entities
{
    public class PlayerMatch
    {
        public string PlayerId { get; set; }
        public Player Player { get; set; }

        public int MatchId { get; set; }
        public Match Match { get; set; }
    }
}