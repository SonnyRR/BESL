namespace BESL.Application.Tournaments.Queries.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Application.Tournaments.Commands.Create;
    using AutoMapper.QueryableExtensions;
    using BESL.Application.Tournaments.Models;
    using Microsoft.EntityFrameworkCore;

    public class CreateTournamentQueryHandler : IRequestHandler<CreateTournamentQuery, CreateTournamentCommand>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CreateTournamentQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<CreateTournamentCommand> Handle(CreateTournamentQuery request, CancellationToken cancellationToken)
        {
            CreateTournamentCommand command = new CreateTournamentCommand()
            {
                Formats = await this.dbContext
                            .TournamentFormats
                                .Include(tf=>tf.Game)
                            .ProjectTo<TournamentFormatSelectListLookupModel>(this.mapper.ConfigurationProvider)
                            .ToListAsync()
            };

            return command;
        }
    }
}
