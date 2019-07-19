namespace BESL.Application.Players.Queries.GetPlayerDetails
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;
    using AutoMapper;

    public class PlayerDetailsQueryHandler : IRequestHandler<GetPlayerDetailsQuery, PlayerDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Player> repository;
        private readonly IMapper mapper;

        public PlayerDetailsQueryHandler(IDeletableEntityRepository<Player> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PlayerDetailsViewModel> Handle(GetPlayerDetailsQuery request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var desiredPlayer = await this.repository
                .AllAsNoTracking()
                .Include(p => p.Claims)
                .SingleOrDefaultAsync(p => p.UserName == request.Username, cancellationToken);

            if (desiredPlayer == null)
            {
                throw new NotFoundException(nameof(Player), request.Username);
            }

            var viewModel = this.mapper.Map<PlayerDetailsViewModel>(desiredPlayer);            
            return viewModel;
        }
    }
}
