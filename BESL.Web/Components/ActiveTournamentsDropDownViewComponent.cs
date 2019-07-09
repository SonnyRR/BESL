namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Tournaments.Queries.GetAllTournaments;

    // Does not work without explicitly tagging the class with a ViewComponentAttribute and a display name.
    [ViewComponent(Name = "ActiveTournamentsDropDownVC")] 
    public class ActiveTournamentsDropDownViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public ActiveTournamentsDropDownViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentActiveTournamentsViewModel = await this.mediator.Send(new GetAllTournamentsQuery());
            return this.View(currentActiveTournamentsViewModel);
        }
    }
}
