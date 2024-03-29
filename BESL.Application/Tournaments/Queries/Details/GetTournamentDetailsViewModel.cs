﻿namespace BESL.Application.Tournaments.Queries.Details
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;    
    using BESL.Entities;
    using static BESL.SharedKernel.GlobalConstants;

    public class GetTournamentDetailsViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TournamentImageUrl { get; set; }

        public bool IsActive { get; set; }

        public bool AreSignupsOpen { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Format { get; set; }

        public string Game { get; set; }

        public bool IsCurrentUserInAnEnrolledTeam { get; set; }

        public ICollection<TournamentTable> TournamentTables { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Tournament, GetTournamentDetailsViewModel>()
                .ForMember(vm => vm.StartDate, o => o.MapFrom(src => src.StartDate.ToString(DATE_FORMAT)))
                .ForMember(vm => vm.EndDate, o => o.MapFrom(src => src.EndDate.ToString(DATE_FORMAT)))
                .ForMember(vm => vm.Format, o => o.MapFrom(src => $"{src.Format.Name}"))
                .ForMember(vm => vm.Game, o => o.MapFrom(src => src.Format.Game.Name))
                .ForMember(vm => vm.TournamentTables, o => o.MapFrom(src => src.Tables.ToList()))
                .ForMember(vm => vm.IsCurrentUserInAnEnrolledTeam,
                    o => o.MapFrom((src, opt, destMember, context) => src.Tables.SelectMany(y => y.TeamTableResults.SelectMany(w => w.Team.PlayerTeams)).Any(i => i.PlayerId == (string)context.Items["CurrentUserId"])));
        }
    }
}
