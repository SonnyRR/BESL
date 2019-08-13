namespace BESL.Application.Games.Queries.GetAllGamesSelectList
{
    using System.Collections.Generic;
    using MediatR;

    public class GetAllGamesSelectListQuery : IRequest<IEnumerable<GameSelectItemLookupModel>>
    {
    }
}
