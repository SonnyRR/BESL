namespace BESL.Application.Tournaments.Queries.GetAllTournamentsSelectList
{
    using System.Collections.Generic;
    using MediatR;

    public class GetAllTournamentsSelectListQuery : IRequest<IEnumerable<TournamentSelectItemLookupModel>>
    {
    }
}
