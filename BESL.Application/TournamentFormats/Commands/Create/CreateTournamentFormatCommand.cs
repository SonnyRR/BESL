namespace BESL.Application.TournamentFormats.Commands.Create
{
    using BESL.Application.Common.Models.Lookups;
    using MediatR;
    using System.Collections.Generic;

    public class CreateTournamentFormatCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int TeamPlayersCount { get; set; }

        public int GameId { get; set; }

        public IEnumerable<GameSelectItemLookupModel> Games { get; set; }
    }
}
