﻿namespace BESL.Entities
{
    using BESL.Entities.Infrastructure;

    public class TeamInvite : BaseDeletableModel<string>
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string SenderUsername { get; set; }

        public string PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
