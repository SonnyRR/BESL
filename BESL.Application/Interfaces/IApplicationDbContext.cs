namespace BESL.Application.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using BESL.Domain.Entities;

    public interface IApplicationDbContext
    {
        DbSet<Setting> Settings { get; set; }

        DbSet<Player> Players { get; set; }

        DbSet<Game> Games { get; set; }

        DbSet<Team> Teams { get; set; }

        DbSet<Match> Matches { get; set; }

        DbSet<PlayerTeam> PlayerTeams { get; set; }

        DbSet<PlayerMatch> PlayerMatches { get; set; }

        DbSet<TeamTableResult> TeamTableResults { get; set; }

        DbSet<Competition> Competitions { get; set; }

        DbSet<CompetitionFormat> CompetitionFormats { get; set; }

        DbSet<CompetitionTable> CompetitionTables { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
