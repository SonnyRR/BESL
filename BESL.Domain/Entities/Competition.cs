namespace BESL.Domain.Entities
{
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Competition : BaseModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int FormatId { get; set; }
        public CompetitionFormat Format { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
