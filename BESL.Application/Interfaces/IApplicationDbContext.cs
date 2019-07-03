namespace BESL.Application.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using BESL.Domain.Entities;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        DbSet<Tournament> Tournaments { get; set; }

        DbSet<TournamentFormat> TournamentFormats { get; set; }

        DbSet<TournamentTable> TournamentTables { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
