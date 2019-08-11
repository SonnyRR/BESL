namespace BESL.Application.Teams.Commands.TransferOwnership
{
    using System.Collections.Generic;
    using MediatR;
    using BESL.Application.Common.Models.Lookups;

    public class TransferTeamOwnershipCommand : IRequest<int>
    {
        public string PlayerId { get; set; }

        public int TeamId { get; set; }

        public IEnumerable<PlayerSelectItemLookupModel> TeamPlayers { get; set; }
    }
}
