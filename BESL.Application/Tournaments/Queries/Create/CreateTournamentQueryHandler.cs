namespace BESL.Application.Tournaments.Queries.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Application.Tournaments.Commands.Create;
    using BESL.Application.Tournaments.Models;
    using BESL.Domain.Entities;

    public class CreateTournamentQueryHandler : IRequestHandler<CreateTournamentQuery, CreateTournamentCommand>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository;
        private readonly IMapper mapper;

        public CreateTournamentQueryHandler(IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository, IMapper mapper)
        {
            this.tournamentFormatsRepository = tournamentFormatsRepository;
            this.mapper = mapper;
        }

        public async Task<CreateTournamentCommand> Handle(CreateTournamentQuery request, CancellationToken cancellationToken)
        {
            CreateTournamentCommand command = new CreateTournamentCommand
            {
                Formats = await this.tournamentFormatsRepository
                            .AllAsNoTracking()
                                .Include(tf => tf.Game)
                            .ProjectTo<TournamentFormatSelectListLookupModel>(this.mapper.ConfigurationProvider)
                            .ToListAsync()
            };

            return command;
        }
    }
}
