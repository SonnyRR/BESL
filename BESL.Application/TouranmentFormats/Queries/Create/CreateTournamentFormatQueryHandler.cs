namespace BESL.Application.TournamentFormats.Queries.Create
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BESL.Application.TournamentFormats.Commands.Create;
    using BESL.Application.Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;
    using BESL.Application.Common.Models;

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
                .ProjectTo<GameSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return new CreateTournamentFormatCommand() { Games = gamesMapped };
        }
    }
}
