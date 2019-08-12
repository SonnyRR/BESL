namespace BESL.Application.Matches.Queries.Modify
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
    using BESL.Application.Matches.Commands.Modify;

    public class ModifyMatchQueryHandler : IRequestHandler<ModifyMatchQuery, ModifyMatchCommand>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IMapper mapper;

        public ModifyMatchQueryHandler(IDeletableEntityRepository<Match> matchesRepository, IMapper mapper)
        {
            this.matchesRepository = matchesRepository;
            this.mapper = mapper;
        }

        public async Task<ModifyMatchCommand> Handle(ModifyMatchQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredMatch = await this.matchesRepository
                .AllAsNoTracking()
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .SingleOrDefaultAsync(m => m.Id == request.MatchId, cancellationToken)
                ?? throw new NotFoundException(nameof(Match), request.MatchId);

            var matchViewModel = this.mapper.Map<ModifyMatchCommand>(desiredMatch);
            return matchViewModel;
        }
    }
}
