﻿namespace BESL.Application.Common.Models.Lookups
{
    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;

    public class TeamsSelectItemLookupModel : IMapFrom<Team>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
