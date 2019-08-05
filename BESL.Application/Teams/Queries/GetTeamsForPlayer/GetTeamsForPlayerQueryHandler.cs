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

    public class GetTeamsForPlayerQueryHandler : IRequestHandler<GetTeamsForPlayerQuery, TeamsForPlayerViewModel>
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<PlayerTeam> playerTeamsRepository;
        private readonly IDeletableEntityRepository<Player> playersRepository;

        public GetTeamsForPlayerQueryHandler(
            IMapper mapper,
            IDeletableEntityRepository<PlayerTeam> playerTeamsRepository,
            IDeletableEntityRepository<Player> playersRepository)
        {
            this.mapper = mapper;
            this.playerTeamsRepository = playerTeamsRepository;
            this.playersRepository = playersRepository;
        }

        public async Task<TeamsForPlayerViewModel> Handle(GetTeamsForPlayerQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await this.CheckIfPlayerWithGivenIdExists(request))
            {
                throw new NotFoundException(nameof(Player), request.UserId);
            }

            var teams = await this.playerTeamsRepository
                .AllAsNoTracking()
                    .Include(x => x.Team)
                .Where(x => x.PlayerId == request.UserId)
                .ToListAsync(cancellationToken);

            var mapped = this.mapper.Map<TeamForPlayerLookupModel[]>(teams);

            var viewModel = new TeamsForPlayerViewModel() { PlayerTeams = mapped };
            return viewModel;
        }

        private async Task<bool> CheckIfPlayerWithGivenIdExists(GetTeamsForPlayerQuery request)
        {
            return await this.playersRepository
                .AllAsNoTracking()
                .AnyAsync(p => p.Id == request.UserId);
        }
    }    
}
