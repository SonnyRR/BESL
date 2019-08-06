namespace BESL.Application.Tournaments.Queries.Enroll
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using BESL.Application.Exceptions;
    using System.Linq;
    using BESL.Application.Tournaments.Commands.Enroll;
    using AutoMapper.QueryableExtensions;
    using BESL.Application.Common.Models.Lookups;

    public class EnrollATeamQueryHandler : IRequestHandler<EnrollATeamQuery, EnrollATeamCommand>
    {
        private readonly IDeletableEntityRepository<Player> playersRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IMapper mapper;

        public EnrollATeamQueryHandler(
            IDeletableEntityRepository<Player> playersRepository,
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<Tournament> tournamentsRepository,
            IMapper mapper)
        {
            this.playersRepository = playersRepository;
            this.teamsRepository = teamsRepository;
            this.tournamentsRepository = tournamentsRepository;
            this.mapper = mapper;
        }

        public async Task<EnrollATeamCommand> Handle(EnrollATeamQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await this.CheckIfUserExists(request))
            {
                throw new NotFoundException(nameof(Player), request.UserId);
            }

            if (await this.CheckIfUserAlreadyHasAnEnrolledTeamInGivenTournament(request))
            {
                // TODO
            }

            var desiredTournament = await this.tournamentsRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == request.TournamentId, cancellationToken)
                ?? throw new NotFoundException(nameof(Tournament), request.UserId);

            var eligibleTeams = await this.teamsRepository
                .AllAsNoTracking()
                .Where(t => t.OwnerId == request.UserId && t.TournamentFormatId == desiredTournament.FormatId)
                .ProjectTo<TeamsSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new EnrollATeamCommand
            {
                UserId = request.UserId,
                TournamentId = request.TournamentId,
                Teams = eligibleTeams
            };
        }

        private async Task<bool> CheckIfUserExists(EnrollATeamQuery request)
        {
            return await this.playersRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Id == request.UserId);
        }

        private async Task<bool> CheckIfUserAlreadyHasAnEnrolledTeamInGivenTournament(EnrollATeamQuery request)
        {
            return await this.teamsRepository
                .AllAsNoTracking()
                .Where(t => t.OwnerId == request.UserId)
                .Include(t => t.CurrentActiveTeamTableResult)
                    .ThenInclude(cattr => cattr.TournamentTable)
                .AnyAsync(t => t.CurrentActiveTeamTableResult.TournamentTable.TournamentId == request.TournamentId);

            //return await this.playersRepository
            //    .AllAsNoTracking()
            //    .Where(p => p.Id == request.UserId)
            //    .Include(p => p.OwnedTeams)
            //        .ThenInclude(t => t.CurrentActiveTeamTableResult)
            //            .ThenInclude(attr => attr.TournamentTable)
            //    .AnyAsync(p => p.OwnedTeams.Any(t => t.CurrentActiveTeamTableResult.TournamentTable.TournamentId == request.TournamentId));
        }
    }
}
