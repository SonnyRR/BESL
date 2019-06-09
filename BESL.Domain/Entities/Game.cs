﻿namespace BESL.Domain.Entities
{
    using BESL.Domain.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game : BaseModel<string>
    {
        
        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
     
    }
}
