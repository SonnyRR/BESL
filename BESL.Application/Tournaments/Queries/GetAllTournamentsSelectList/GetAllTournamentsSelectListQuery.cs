namespace BESL.Application.Tournaments.Queries.GetAllTournamentsSelectList
{
    using System.Collections.Generic;
    using MediatR;
    using BESL.Application.Common.Models.Lookups;

    public class GetAllTournamentsSelectListQuery : IRequest<IEnumerable<TournamentSelectItemLookupModel>>
    {
    }
}
