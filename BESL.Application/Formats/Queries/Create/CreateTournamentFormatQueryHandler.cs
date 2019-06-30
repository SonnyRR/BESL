namespace BESL.Application.Formats.Queries.Create
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BESL.Application.Formats.Commands.Create;
    using BESL.Application.Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateTournamentFormatQueryHandler : IRequestHandler<CreateTournamentFormatQuery, CreateTournamentFormatCommand>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CreateTournamentFormatQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<CreateTournamentFormatCommand> Handle(CreateTournamentFormatQuery request, CancellationToken cancellationToken)
        {
            var gamesMapped = await this.dbContext
                .Games
                .ProjectTo<GameLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return new CreateTournamentFormatCommand() { Games = gamesMapped };
        }
    }
}
