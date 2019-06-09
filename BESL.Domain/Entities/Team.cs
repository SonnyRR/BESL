namespace BESL.Domain.Entities
{
    using System;
    using System.Collections.Generic;


    public class Team
    {
        public string Name { get; set; }

        public Player Owner { get; set; }

        public ICollection<Player> Players { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }


    }
}
