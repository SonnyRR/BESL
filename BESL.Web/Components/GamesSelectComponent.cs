namespace BESL.Web.Components
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using BESL.Application.Games.Queries.GetAllGamesSelectList;

    [ViewComponent(Name = "GamesSelectList")]
    public class GamesSelectListComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public GamesSelectListComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var items = await GetItemsAsync();
            return View(items);
        }
        private async Task<List<SelectListItem>> GetItemsAsync()
        {
            var gamesLookups = await this.mediator.Send(new GetAllGamesSelectListQuery());
            var mappedGames = gamesLookups
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            return mappedGames;
        }
    }
}
