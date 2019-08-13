namespace BESL.Web.Components
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using BESL.Application.Teams.Commands.InvitePlayer;

    public class InvitePlayerModalViewComponent : ViewComponent
    {
#pragma warning disable CS1998
        public async Task<IViewComponentResult> InvokeAsync(int teamId)
        {
            var viewModel = new InvitePlayerCommand { TeamId = teamId };
            return this.View(viewModel);
        }
#pragma warning restore CS1998
    }
}
