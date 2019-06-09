namespace BESL.Domain.Entities
{
    using ISO3166;
    using System;
    using System.Collections.Generic;

    public class Player : ApplicationUser
    {

        public ICollection<Team> OwnedTeams { get; set; } = new HashSet<Team>();

        public ICollection<PlayerTeam> PlayerTeams { get; set; } = new HashSet<PlayerTeam>();

    }
}
