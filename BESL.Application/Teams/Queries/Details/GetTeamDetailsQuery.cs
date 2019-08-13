namespace BESL.Application.Teams.Queries.Details
{
    using MediatR;

    public class GetTeamDetailsQuery : IRequest<GetTeamDetailsViewModel>
    {
        public int Id { get; set; }
    }
}
