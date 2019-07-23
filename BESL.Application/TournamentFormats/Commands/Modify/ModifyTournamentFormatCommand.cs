namespace BESL.Application.TournamentFormats.Commands.Modify
{
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;
    using MediatR;

    public class ModifyTournamentFormatCommand : IRequest<int>, IMapFrom<TournamentFormat>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TeamPlayersCount { get; set; }

        public string GameName { get; set; }

    }
}
