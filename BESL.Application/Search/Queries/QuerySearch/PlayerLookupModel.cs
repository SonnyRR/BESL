﻿namespace BESL.Application.Search.Queries.QuerySearch
{
    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;

    public class PlayerLookupModel : IMapFrom<Player>
    {
        public string UserName { get; set; }
    }
}
