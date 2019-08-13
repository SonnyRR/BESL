namespace BESL.Application.TournamentFormats.Commands.Create
{
    using System.Collections.Generic;
    using MediatR;
    using BESL.Application.Games.Queries.GetAllGamesSelectList;

    public class CreateTournamentFormatCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int TeamPlayersCount { get; set; }

        public int GameId { get; set; }

        public IEnumerable<GameSelectItemLookupModel> Games { get; set; }
    }
}
