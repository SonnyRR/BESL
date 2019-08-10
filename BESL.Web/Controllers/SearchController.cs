namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using BESL.Application.Search.Queries;

    public class SearchController : BaseController
    {
        public async Task<IActionResult> Index([FromQuery]string query)
        {
            var viewModel = await this.Mediator.Send(new SearchQuery { Query = query });
            return this.View(viewModel);
        }
    }
}
