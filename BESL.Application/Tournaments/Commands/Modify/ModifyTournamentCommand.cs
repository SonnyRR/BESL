namespace BESL.Application.Tournaments.Commands.Modify
{
    using System;

    using MediatR;
    using Microsoft.AspNetCore.Http;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;
    using AutoMapper;

    public class ModifyTournamentCommand : IRequest<int>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TournamentImageUrl { get; set; }

        public IFormFile TournamentImage { get; set; }

        public string FormatName { get; set; }

        public bool IsActive { get; set; }

        public bool AreSignupsOpen { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Tournament, ModifyTournamentCommand>()
                .ForMember(x => x.FormatName, o => o.MapFrom(src => $"{src.Format.Name} - {src.Format.Game.Name}"));
        }
    }
}
