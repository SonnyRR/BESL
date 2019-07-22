namespace BESL.Application.Tournaments.Queries.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tournaments.Commands.Modify;
    using BESL.Domain.Entities;

    public class TournamentModifyQuery : IRequest<ModifyTournamentCommand>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<TournamentModifyQuery, ModifyTournamentCommand>
        {
            private readonly IDeletableEntityRepository<Tournament> repository;
            private readonly IMapper mapper;

            public Handler(IDeletableEntityRepository<Tournament> repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<ModifyTournamentCommand> Handle(TournamentModifyQuery request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var desiredTournament = await this.repository
                    .AllAsNoTrackingWithDeleted()
                        .Include(x => x.Format)
                        .Include(x => x.Game)
                    .FirstOrDefaultAsync(t => t.Id == request.Id);

                if (desiredTournament == null)
                {
                    throw new NotFoundException(nameof(Tournament), request.Id);
                }

                var modifyCommand = this.mapper.Map<ModifyTournamentCommand>(desiredTournament);

                return modifyCommand;
            }
        }
    }
}
