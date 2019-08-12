namespace BESL.Application.TeamTableResults.Commands.Activate
{
    using MediatR;
    
    public class ActivateTeamTableResultCommand : IRequest<int>
    {
        public int TeamTableResultId { get; set; }
    }
}
