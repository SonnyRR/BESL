namespace BESL.Application.TournamentFormats.Queries.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.TournamentFormats.Commands.Modify;
    using BESL.Domain.Entities;   

    public class ModifyTournamentFormatQueryHandler : IRequestHandler<ModifyTournamentFormatQuery, ModifyTournamentFormatCommand>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository;
        private readonly IMapper mapper;

        public ModifyTournamentFormatQueryHandler(IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository, IMapper mapper)
        {
            this.tournamentFormatsRepository = tournamentFormatsRepository;
            this.mapper = mapper;
        }

        public async Task<ModifyTournamentFormatCommand> Handle(ModifyTournamentFormatQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredFormat = await this.tournamentFormatsRepository
                .AllAsNoTrackingWithDeleted()
                .Include(tf => tf.Game)
                .SingleOrDefaultAsync(f => f.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(TournamentFormat), request.Id);

            var viewModel = this.mapper.Map<ModifyTournamentFormatCommand>(desiredFormat);
            return viewModel;
        }
    }
}