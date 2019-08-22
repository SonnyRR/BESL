namespace BESL.Application.Notifications.Queries.GetNotificationsForPlayer
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class GetNotificationsForPlayerQueryHandler : IRequestHandler<GetNotificationsForPlayerQuery, PlayerNotificationsViewModel>
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Notification> notificationRepository;

        public GetNotificationsForPlayerQueryHandler(IDeletableEntityRepository<Notification> notificationRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.notificationRepository = notificationRepository;
        }

        public async Task<PlayerNotificationsViewModel> Handle(GetNotificationsForPlayerQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var notificationLookups = await this.notificationRepository
                .AllAsNoTracking()
                .Where(n => n.PlayerId == request.UserId)
                .OrderByDescending(n => n.CreatedOn)
                .ProjectTo<PlayerNotificationLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new PlayerNotificationsViewModel { Notifications = notificationLookups };
            return viewModel;
        }
    }
}
