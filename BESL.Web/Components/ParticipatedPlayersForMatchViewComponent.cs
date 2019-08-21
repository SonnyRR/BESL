namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Players.Queries.GetMatchParticipatedPlayers;

    public class ParticipatedPlayersForMatchViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public ParticipatedPlayersForMatchViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int matchId)
        {
            var viewModel = await this.mediator.Send(new GetMatchParticipatedPlayersQuery { MatchId = matchId });
            return this.View(viewModel);
        }
    }
}
