namespace BESL.Application.TournamentFormats.Queries.Modify
{
    using FluentValidation;

    public class ModifyTournamentFormatQueryValidator : AbstractValidator<ModifyTournamentFormatViewModel>
    {
        private const int NAME_MIN_LENGTH = 3, NAME_MAX_LENGTH = 25;
        private const int DESC_MIN_LENGTH = 25, DESC_MAX_LENGTH = 500;

        private const int TEAM_PLAYERS_MIN_COUNT = 1, TEAM_PLAYERS_MAX_COUNT = 12;

        private const string NAME_LENGTH_MSG = "Format name length must be between {0} and {1} characters long!";
        private const string DESC_LENGTH_MSG = "Format description length must be between {0} and {1} characters long!";

        private const string TEAM_PLAYERS_COUNT_MSG = "Team players count must be between {0} and {1}!";

        public ModifyTournamentFormatQueryValidator()
        {
            RuleFor(f => f.Name)
                .MinimumLength(NAME_MIN_LENGTH)
                .MaximumLength(NAME_MAX_LENGTH)
                .NotEmpty()
                .WithMessage(string.Format(NAME_LENGTH_MSG, NAME_MIN_LENGTH, NAME_MAX_LENGTH));

            RuleFor(f => f.Description)
                .MinimumLength(DESC_MIN_LENGTH)
                .MaximumLength(DESC_MAX_LENGTH)
                .NotEmpty()
                .WithMessage(string.Format(DESC_LENGTH_MSG, DESC_MIN_LENGTH, DESC_MAX_LENGTH));

            RuleFor(f => f.TeamPlayersCount)
                .NotEmpty()
                .InclusiveBetween(TEAM_PLAYERS_MIN_COUNT, TEAM_PLAYERS_MAX_COUNT)
                .WithMessage(string.Format(TEAM_PLAYERS_COUNT_MSG, TEAM_PLAYERS_MIN_COUNT, TEAM_PLAYERS_MAX_COUNT));

            RuleFor(f => f.GameId)
                .NotEmpty();
        }
    }
}
