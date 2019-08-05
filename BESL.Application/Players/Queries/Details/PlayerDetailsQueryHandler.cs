namespace BESL.Application.Players.Queries.Details
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class PlayerDetailsQueryHandler : IRequestHandler<GetPlayerDetailsQuery, PlayerDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Player> playersRepository;
        private readonly IMapper mapper;

        public PlayerDetailsQueryHandler(IDeletableEntityRepository<Player> playersRepository, IMapper mapper)
        {
            this.playersRepository = playersRepository;
            this.mapper = mapper;
        }

        public async Task<PlayerDetailsViewModel> Handle(GetPlayerDetailsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredPlayer = await this.playersRepository
                .AllAsNoTracking()
                .Include(p => p.Claims)
                .SingleOrDefaultAsync(p => p.UserName == request.Username, cancellationToken)
                ?? throw new NotFoundException(nameof(Player), request.Username);

            var viewModel = this.mapper.Map<PlayerDetailsViewModel>(desiredPlayer);
            return viewModel;
        }
    }
}
