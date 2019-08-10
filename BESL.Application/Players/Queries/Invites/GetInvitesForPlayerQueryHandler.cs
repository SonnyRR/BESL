namespace BESL.Application.Players.Queries.Invites
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class GetInvitesForPlayerQueryHandler : IRequestHandler<GetInvitesForPlayerQuery, InvitesViewModel>
    {
        private readonly IDeletableEntityRepository<TeamInvite> teamInvitesRepository;
        private readonly IMapper mapper;
        private readonly IUserAcessor userAcessor;

        public GetInvitesForPlayerQueryHandler(IDeletableEntityRepository<TeamInvite> teamInvitesRepository, IMapper mapper, IUserAcessor userAcessor)
        {
            this.teamInvitesRepository = teamInvitesRepository;
            this.mapper = mapper;
            this.userAcessor = userAcessor;
        }

        public async Task<InvitesViewModel> Handle(GetInvitesForPlayerQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var inviteLookups = await this.teamInvitesRepository
                .AllAsNoTracking()
                .Where(i => i.PlayerId == userAcessor.UserId)
                .ProjectTo<InviteLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new InvitesViewModel { Invites = inviteLookups };
            return viewModel;
        }
    }
}
