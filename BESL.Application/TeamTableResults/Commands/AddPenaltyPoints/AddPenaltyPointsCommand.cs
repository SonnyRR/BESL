namespace BESL.Application.TeamTableResults.Commands.AddPenaltyPoints
{
    using MediatR;

    public class AddPenaltyPointsCommand : IRequest<int>
    {
        public int TeamTableResultId { get; set; }

        public int PenaltyPoints { get; set; }
    }
}
