namespace BESL.Domain.Entities
{
    using ISO3166;
    using System;
    using System.Collections.Generic;

    public class Player : ApplicationUser
    {

        public virtual ICollection<Team> OwnedTeams { get; set; } = new HashSet<Team>();

        public virtual ICollection<PlayerTeam> PlayerTeams { get; set; } = new HashSet<PlayerTeam>();

        public virtual ICollection<PlayerMatch> PlayerMatches { get; set; } = new HashSet<PlayerMatch>();

    }
}
