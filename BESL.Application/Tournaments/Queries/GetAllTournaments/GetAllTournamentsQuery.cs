namespace BESL.Application.Tournaments.Queries.GetAllTournaments
{
    using BESL.Application.Common.Models.View;
    using MediatR;

    public class GetAllTournamentsQuery : IRequest<AllTournamentsViewModel>
    {
    }
}
