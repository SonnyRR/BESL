namespace BESL.Application.Formats.Queries.GetAll
{
    using System.Collections.Generic;

    public class GetAllTournamentFormatsQueryViewModel
    {
        public GetAllTournamentFormatsQueryViewModel(IEnumerable<TournamentFormatLookupModel> tournamentFormats)
        {
            this.TournamentFormats = tournamentFormats;
        }

        public IEnumerable<TournamentFormatLookupModel> TournamentFormats { get; set; }
    }
}
