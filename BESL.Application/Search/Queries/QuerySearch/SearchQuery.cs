namespace BESL.Application.Search.Queries.QuerySearch
{
    using MediatR;

    public class SearchQuery : IRequest<SearchQueryViewModel>
    {
        public string Query { get; set; }
    }
}
