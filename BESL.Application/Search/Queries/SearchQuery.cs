namespace BESL.Application.Search.Queries
{
    using MediatR;

    public class SearchQuery : IRequest<SearchQueryViewModel>
    {
        public string Query { get; set; }
    }
}
