namespace BESL.Application.Matches.Queries.Details
{
    using MediatR;

    public class GetMatchDetailsQuery : IRequest<GetMatchDetailsViewModel>
    {
        public int Id { get; set; }
    }
}
