namespace BESL.Web.Components
{
    using System.Threading.Tasks;
    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Commands.InvitePlayer;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class InvitePlayerModalViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int teamId)
        {
            var viewModel = new InvitePlayerCommand { TeamId = teamId };
            return this.View(viewModel);
        }
    }
}
