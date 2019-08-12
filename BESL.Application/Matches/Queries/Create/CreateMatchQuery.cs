namespace BESL.Application.Matches.Queries.Create
{
    using MediatR;
    using BESL.Application.Matches.Commands.Create;

    public class CreateMatchQuery : IRequest<CreateMatchCommand>
    {
        public int TournamentTableId { get; set; }

        public int PlayWeekId { get; set; }
    }
}
