namespace BESL.Application.Teams.Queries.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Common.Models;
    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Commands.Create;
    using BESL.Domain.Entities;

    public class CreateTeamQuery : IRequest<CreateTeamCommand>
    {
        public class Handler : IRequestHandler<CreateTeamQuery, CreateTeamCommand>
        {
            private readonly IDeletableEntityRepository<TournamentFormat> repository;
            private readonly IMapper mapper;

            public Handler(IDeletableEntityRepository<TournamentFormat> repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<CreateTeamCommand> Handle(CreateTeamQuery request, CancellationToken cancellationToken)
            {
                var model = await this.repository
                    .AllAsNoTracking()
                        .Include(tf=>tf.Game)
                    .ProjectTo<TournamentFormatSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var command = new CreateTeamCommand()
                {
                    Formats = model
                };

                return command;
            }
        }
    }
}
