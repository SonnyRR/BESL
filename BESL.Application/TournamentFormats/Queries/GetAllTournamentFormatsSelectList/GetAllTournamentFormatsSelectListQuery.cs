namespace BESL.Application.TournamentFormats.Queries.GetAllTournamentFormatsSelectList
{
    using System.Collections.Generic;
    using MediatR;

    public class GetAllTournamentFormatsSelectListQuery :  IRequest<IEnumerable<TournamentFormatSelectItemLookupModel>>
    {
    }
}
