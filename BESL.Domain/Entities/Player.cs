namespace BESL.Domain.Entities
{
    using ISO3166;
    using System;
    using System.Collections.Generic;

    public class Player : ApplicationUser
    {
        public Guid Id { get; set; }

        public ICollection<Team> Teams { get; set; }

        public Country Country { get; set; }

    }
}
