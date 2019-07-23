namespace BESL.Application.TournamentFormats.Commands.Modify
{
    using MediatR;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class ModifyTournamentFormatCommand : IRequest<int>, IMapFrom<TournamentFormat>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TeamPlayersCount { get; set; }

        public string GameName { get; set; }

        public int GameId { get; set; }
    }
}
