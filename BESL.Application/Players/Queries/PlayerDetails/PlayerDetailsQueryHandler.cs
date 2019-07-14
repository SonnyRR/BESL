namespace BESL.Application.Players.Queries.PlayerDetails
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using static BESL.Common.GlobalConstants;
    public class PlayerDetailsQueryHandler : IRequestHandler<PlayerDetailsQuery, PlayerDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Player> repository;

        public PlayerDetailsQueryHandler(IDeletableEntityRepository<Player> repository)
        {
            this.repository = repository;
        }

        public async Task<PlayerDetailsViewModel> Handle(PlayerDetailsQuery request, CancellationToken cancellationToken)
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

            var viewModel = new PlayerDetailsViewModel()
            {
                Username = desiredPlayer.UserName,
                IsVACBanned = desiredPlayer.Claims.Any(c => c.ClaimType == IS_VAC_BANNED_CLAIM_TYPE),
                AvatarFullUrl = desiredPlayer.Claims.SingleOrDefault(c => c.ClaimType == PROFILE_AVATAR_CLAIM_TYPE)?.ClaimValue
            };

            return viewModel;
        }
    }
}
