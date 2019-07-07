namespace BESL.Application.Games.Queries.GetAllGamesSelectList
{
    using System.Collections.Generic;
    using MediatR;
    using BESL.Application.Common.Models;

    public class GetAllGamesSelectListQuery : IRequest<IEnumerable<GameSelectItemLookupModel>>
    {
    }
}
