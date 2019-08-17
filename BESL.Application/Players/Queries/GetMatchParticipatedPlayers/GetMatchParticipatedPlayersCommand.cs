﻿namespace BESL.Application.Players.Queries.GetMatchParticipatedPlayers
{
    using System.Collections.Generic;
    using MediatR;
    using BESL.Application.Common.Models.Lookups;

    public class GetMatchParticipatedPlayersCommand : IRequest<MatchParticipatedPlayersViewModel>
    {
        public int MatchId { get; set; }
    }
}
