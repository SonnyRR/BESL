namespace BESL.Application.Teams.Queries.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Commands.Create;
    using BESL.Domain.Entities;

    public class CreateTeamQueryHandler : IRequestHandler<CreateTeamQuery, CreateTeamCommand>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository;
        private readonly IMapper mapper;

        public CreateTeamQueryHandler(IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository, IMapper mapper)
        {
            this.tournamentFormatsRepository = tournamentFormatsRepository;
            this.mapper = mapper;
        }

        public async Task<CreateTeamCommand> Handle(CreateTeamQuery request, CancellationToken cancellationToken)
        {
            var model = await this.tournamentFormatsRepository
                .AllAsNoTracking()
                    .Include(tf => tf.Game)
                .ProjectTo<TournamentFormatSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var command = new CreateTeamCommand { Formats = model };

            return command;
        }
    }
}
