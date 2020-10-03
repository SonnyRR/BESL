namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Notifications.Queries.GetNotificationsForPlayer;

    public class PlayerNotificationsViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public PlayerNotificationsViewComponent(IMediator mediator) => this.mediator = mediator;

        public async Task<IViewComponentResult> InvokeAsync(string userId)
            => this.View(await this.mediator.Send(new GetNotificationsForPlayerQuery() { UserId = userId }));        
    }
}
