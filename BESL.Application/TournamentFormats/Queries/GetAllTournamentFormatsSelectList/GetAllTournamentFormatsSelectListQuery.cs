namespace BESL.Application.TournamentFormats.Queries.GetAllTournamentFormatsSelectList
{
    using System.Collections.Generic;
    using MediatR;
    using BESL.Application.Common.Models.Lookups;

    public class GetAllTournamentFormatsSelectListQuery : IRequest<IEnumerable<TournamentFormatSelectItemLookupModel>>
    {
    }
}
