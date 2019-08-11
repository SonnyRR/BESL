namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Teams.Queries.TransferOwnership;

    public class TransferTeamOwnershipViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public TransferTeamOwnershipViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int teamId)
        {
            var viewModel = await this.mediator.Send(new TransferTeamOwnershipQuery { TeamId = teamId });
            return this.View(viewModel);
        }
    }
}
