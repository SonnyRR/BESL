namespace BESL.Web.Components
{
    using System.Threading.Tasks;
    using BESL.Application.Players.Queries.GetMatchParticipatedPlayers;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;   

    public class GetParticipatedPlayersForMatchViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public GetParticipatedPlayersForMatchViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int matchId)
        {
            var viewModel = await this.mediator.Send(new GetMatchParticipatedPlayersCommand { MatchId = matchId });
            return this.View(viewModel);
        }
    }
}
