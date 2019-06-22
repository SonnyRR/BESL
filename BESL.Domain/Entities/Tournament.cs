namespace BESL.Domain.Entities
{
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Tournament : BaseDeletableModel<int>
    {
        public Tournament()
        {
            this.IsActive = true;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int FormatId { get; set; }
        public TournamentFormat Format { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public virtual ICollection<TournamentTable> Tables { get; set; } = new HashSet<TournamentTable>();
    }
}
