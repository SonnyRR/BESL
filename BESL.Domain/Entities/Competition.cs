namespace BESL.Domain.Entities
{
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Competition : BaseDeletableModel<int>
    {
        public Competition()
        {
            this.IsActive = true;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int FormatId { get; set; }
        public CompetitionFormat Format { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public virtual ICollection<CompetitionTable> Tables { get; set; } = new HashSet<CompetitionTable>();
    }
}
