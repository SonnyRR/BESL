namespace BESL.Application.Matches.Queries.Modify
{
    using BESL.Application.Matches.Commands.Modify;
    using MediatR;

    public class ModifyMatchQuery : IRequest<ModifyMatchCommand>
    {
        public int MatchId { get; set; }
    }
}
