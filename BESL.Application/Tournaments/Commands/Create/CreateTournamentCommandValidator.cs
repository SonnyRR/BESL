namespace BESL.Application.Tournaments.Commands.Create
{
    using FluentValidation;

    using BESL.Application.Interfaces;
    using BESL.Application.Infrastructure.Validators;

    public class CreateTournamentCommandValidator : AbstractValidator<CreateTournamentCommand>
    {
        private const int NAME_MIN_LENGTH = 10, NAME_MAX_LENGTH = 50;
        private const int DESC_MIN_LENGTH = 50, DESC_MAX_LENGTH = 500;

        private const string NAME_LENGTH_MSG = "Tournament name length must be between {0} and {1} characters long!";
        private const string DESC_LENGTH_MSG = "Tournament description length must be between {0} and {1} characters long!";

        public CreateTournamentCommandValidator(IFileValidate fileValidate)
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .Length(NAME_MIN_LENGTH, NAME_MAX_LENGTH)
                .WithMessage(string.Format(NAME_LENGTH_MSG, NAME_MIN_LENGTH, NAME_MAX_LENGTH));

            RuleFor(t => t.Description)
                .NotEmpty()
                .Length(DESC_MIN_LENGTH, DESC_MAX_LENGTH)
                .WithMessage(string.Format(DESC_LENGTH_MSG, DESC_MIN_LENGTH, DESC_MAX_LENGTH));

            RuleFor(t => t.StartDate)
                .GreaterThan(t => new System.DateTime(2015, 1, 1))
                .WithMessage("edede");

            RuleFor(t => t.TournamentImage)
                .NotEmpty()
                .NotNull()
                .SetValidator(new CustomGameImageFileValidator(fileValidate));
        }
    }
}
