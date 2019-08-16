namespace BESL.Application.Players.Queries.GetMatchParticipatedPlayers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using System.Linq;

    public class GetMatchParticipatedPlayersCommandHandler : IRequestHandler<GetMatchParticipatedPlayersCommand, IEnumerable<PlayerSelectItemLookupModel>>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IMapper mapper;

        public GetMatchParticipatedPlayersCommandHandler(IDeletableEntityRepository<Match> matchesRepository, IMapper mapper)
        {
            this.matchesRepository = matchesRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PlayerSelectItemLookupModel>> Handle(GetMatchParticipatedPlayersCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredMatch = await this.matchesRepository
                .AllAsNoTracking()
                .Include(m => m.ParticipatedPlayers)
                .ThenInclude(pp => pp.Player)
                .SingleOrDefaultAsync(m => m.Id == request.MatchId)
                ?? throw new NotFoundException(nameof(Match), request.MatchId);

            var mapped = this.mapper.Map<IEnumerable<PlayerSelectItemLookupModel>>(desiredMatch.ParticipatedPlayers.Select(x => x.Player));
            return mapped;
        }
    }
}
