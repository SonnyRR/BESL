﻿namespace BESL.Application.TournamentFormats.Queries.GetAllTournamentFormats
{
    using AutoMapper;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;

    public class TournamentFormatLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TotalPlayersCount { get; set; }

        public int TeamPlayersCount { get; set; }

        public int GameId { get; set; }

        public string Game { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TournamentFormat, TournamentFormatLookupModel>()
                .ForMember(tflm => tflm.Game, o => o.MapFrom(tf => tf.Game.Name));
        }
    }
}
