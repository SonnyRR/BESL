namespace BESL.Application.Players.Queries.GetMatchParticipatedPlayers
{
    using System.Collections.Generic;
    using BESL.Application.Common.Models.Lookups;

    public class MatchParticipatedPlayersViewModel
    {
        public IEnumerable<PlayerSelectItemLookupModel> Players { get; set; }
    }
}
