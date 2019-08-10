namespace BESL.Application.Teams.Queries.GetTeamsForPlayer
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
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

            if (!await CommonCheckHelper.CheckIfPlayerExists(request.UserId, playersRepository))
            {
                throw new NotFoundException(nameof(Player), request.UserId);
            }

            var teams = await this.playerTeamsRepository
                .AllAsNoTrackingWithDeleted()
                    .Include(x => x.Team)
                .Where(x => x.PlayerId == request.UserId && x.IsDeleted == request.WithDeleted)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync(cancellationToken);

            var mapped = this.mapper.Map<TeamForPlayerLookupModel[]>(teams);

            var viewModel = new TeamsForPlayerViewModel() { PlayerTeams = mapped };
            return viewModel;
        }
    }    
}
