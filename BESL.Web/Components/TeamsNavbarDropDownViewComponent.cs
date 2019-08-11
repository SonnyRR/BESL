namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Queries.GetTeamsForPlayer;

    public class TeamsNavbarDropDownViewComponent : ViewComponent
    {
        private IMediator mediator;
        private IUserAcessor userAcessor;

        public TeamsNavbarDropDownViewComponent(IMediator mediator, IUserAcessor userAcessor)
        {
            this.mediator = mediator;
            this.userAcessor = userAcessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var viewModel = await this.mediator.Send(new GetTeamsForPlayerQuery { UserId = this.userAcessor.UserId });
                return this.View(viewModel);
            }

            return this.View(new TeamsForPlayerViewModel());
        }
    }
}
