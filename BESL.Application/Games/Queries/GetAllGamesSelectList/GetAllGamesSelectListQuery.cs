namespace BESL.Application.Games.Queries.GetAllGamesSelectList
{
    using MediatR;
    using System.Collections.Generic;

    public class GetAllGamesSelectListQuery : IRequest<IEnumerable<GameLookupModel>>
    {
    }
}
