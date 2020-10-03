namespace BESL.Application.Teams.Queries.GetTeamsForPlayer
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
    using BESL.Entities;
    using BESL.Application.Exceptions;
    using BESL.Application.Infrastructure;

    public class GetTeamsForPlayerQueryHandler : IRequestHandler<GetTeamsForPlayerQuery, TeamsForPlayerViewModel>
    {
        private readonly IDeletableEntityRepository<PlayerTeam> playerTeamsRepository;
        private readonly IDeletableEntityRepository<Player> playersRepository;
        private readonly IMapper mapper;

        public GetTeamsForPlayerQueryHandler(
            IMapper mapper,
            IDeletableEntityRepository<PlayerTeam> playerTeamsRepository,
            IDeletableEntityRepository<Player> playersRepository)
        {
            this.playerTeamsRepository = playerTeamsRepository;
            this.playersRepository = playersRepository;
            this.mapper = mapper;
        }

        public async Task<TeamsForPlayerViewModel> Handle(GetTeamsForPlayerQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckHelper.CheckIfPlayerExists(request.UserId, this.playersRepository))
            {
                throw new NotFoundException(nameof(Player), request.UserId);
            }

            var teams = await (request.WithDeleted
                ? this.playerTeamsRepository.AllAsNoTrackingWithDeleted()
                : this.playerTeamsRepository.AllAsNoTracking())
                .Include(x => x.Team)
                .Where(x => x.PlayerId == request.UserId)
                .OrderByDescending(x => x.CreatedOn)
                .ProjectTo<TeamForPlayerLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new TeamsForPlayerViewModel { PlayerTeams = teams };
            return viewModel;
        }
    }    
}
