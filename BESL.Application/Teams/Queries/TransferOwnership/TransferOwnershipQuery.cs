namespace BESL.Application.Teams.Queries.TransferOwnership
{
    using MediatR;
    using BESL.Application.Teams.Commands.TransferOwnership;

    public class TransferTeamOwnershipQuery : IRequest<TransferTeamOwnershipCommand>
    {
        public int TeamId { get; set; }
    }
}
