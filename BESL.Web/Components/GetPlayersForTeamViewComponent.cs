namespace BESL.Web.Components
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class GetPlayersForTeamViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public GetPlayersForTeamViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(string teamId)
        {
            // TODO
            return this.View();
        }

    }
}
