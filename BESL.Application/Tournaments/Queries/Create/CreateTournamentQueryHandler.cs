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
    using BESL.Domain.Entities;

    public class CreateTournamentQueryHandler : IRequestHandler<CreateTournamentQuery, CreateTournamentCommand>
    {
        private readonly IDeletableEntityRepository<Tournament> repository;
        private readonly IMapper mapper;

        public CreateTournamentQueryHandler(IDeletableEntityRepository<Tournament> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CreateTournamentCommand> Handle(CreateTournamentQuery request, CancellationToken cancellationToken)
        {
            CreateTournamentCommand command = new CreateTournamentCommand()
            {
                Formats = await this.repository
                            .AllAsNoTracking()
                                .Include(tf=>tf.Game)
                            .ProjectTo<TournamentFormatSelectListLookupModel>(this.mapper.ConfigurationProvider)
                            .ToListAsync()
            };

            return command;
        }
    }
}
