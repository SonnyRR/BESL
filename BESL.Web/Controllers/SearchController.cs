namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using BESL.Application.Search.Queries.QuerySearch;

    public class SearchController : BaseController
    {
        public async Task<IActionResult> Index([FromQuery]string query)
            => this.View(await this.Mediator.Send(new SearchQuery { Query = query }));
    }
}
