namespace BESL.Domain.Entities
{
    using BESL.Domain.Infrastructure;

    public class CompetitionFormat : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int TotalPlayersCount { get; set; }

        public int TeamPlayersCount { get; set; }
    }
}