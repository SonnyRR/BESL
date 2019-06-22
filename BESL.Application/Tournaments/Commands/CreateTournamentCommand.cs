namespace BESL.Application.Tournaments.Commands
{
    using AutoMapper;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;
    using MediatR;

    public class CreateTournamentCommand : IRequest, IHaveCustomMapping
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int FormatId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            //configuration.CreateMap<CreateTournamentCommand, Tournament>
        }
    }
}
