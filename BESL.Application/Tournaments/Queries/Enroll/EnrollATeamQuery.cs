namespace BESL.Application.Tournaments.Queries.Enroll
{
    using MediatR;
    using BESL.Application.Tournaments.Commands.Enroll;

    public class EnrollATeamQuery : IRequest<EnrollATeamCommand>
    {
        public int TournamentId { get; set; }
    }
}
