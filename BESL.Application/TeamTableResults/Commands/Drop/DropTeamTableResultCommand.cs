namespace BESL.Application.TeamTableResults.Commands.Drop
{
    using MediatR;

    public class DropTeamTableResultCommand : IRequest<int>
    {
        public int TeamTableResultId { get; set; }
    }
}
