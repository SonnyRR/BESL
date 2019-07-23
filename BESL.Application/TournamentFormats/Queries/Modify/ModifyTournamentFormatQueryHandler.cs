namespace BESL.Application.TournamentFormats.Queries.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Application.TournamentFormats.Commands.Modify;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;   

    public class ModifyTournamentFormatQueryHandler : IRequestHandler<ModifyTournamentFormatQuery, ModifyTournamentFormatCommand>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> repository;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public ModifyTournamentFormatQueryHandler(IDeletableEntityRepository<TournamentFormat> repository, IMapper mapper, IMediator mediator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<ModifyTournamentFormatCommand> Handle(ModifyTournamentFormatQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var desiredFormat = await this.repository
                .AllAsNoTrackingWithDeleted()
                    .Include(tf => tf.Game)
                .SingleOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            if (desiredFormat == null)
            {
                throw new NotFoundException(nameof(TournamentFormat), request.Id);
            }

            var viewModel = this.mapper.Map<ModifyTournamentFormatCommand>(desiredFormat);

            return viewModel;
        }
    }
}