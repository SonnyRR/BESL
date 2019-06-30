namespace BESL.Application.Formats.Commands.Create
{
    using System.Collections.Generic;
    using BESL.Application.Formats.Queries.Create;
    using MediatR;

    public class CreateTournamentFormatCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int TotalPlayersCount { get; set; }

        public int TeamPlayersCount { get; set; }

        public int GameId { get; set; }

        public IEnumerable<GameLookupModel> Games { get; set; }
    }
}
