namespace BESL.Application.TournamentFormats.Commands.Create
{
    using FluentValidation;
    using static Validation.Constants;

    public class CreateTournamentFormatValidator : AbstractValidator<CreateTournamentFormatCommand>
    {        
        public CreateTournamentFormatValidator()
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
