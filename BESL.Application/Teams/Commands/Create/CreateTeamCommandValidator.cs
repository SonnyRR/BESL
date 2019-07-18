namespace BESL.Application.Teams.Commands.Create
{
    using BESL.Application.Infrastructure.Validators;
    using BESL.Application.Interfaces;
    using FluentValidation;

    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        private const int NAME_MIN_LENGTH = 3, NAME_MAX_LENGTH = 35;
        private const int DESC_MAX_LENGTH = 1000;
        private const int URL_MAX_LENGTH = 35;

        private const string NAME_LENGTH_MSG = "Team name length must be between {0} and {1} characters long!";
        private const string DESC_LENGTH_MSG = "Team description length must not be above {0} characters long!";
        private const string URL_LENGTH_MSG = "Team homepage url length must not be above {0} characters long!";

        public CreateTeamCommandValidator(IFileValidate fileValidate)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(NAME_MIN_LENGTH, NAME_MAX_LENGTH)
                .WithMessage(string.Format(NAME_LENGTH_MSG, NAME_MIN_LENGTH, NAME_MAX_LENGTH));

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(DESC_MAX_LENGTH)
                .WithMessage(string.Format(DESC_LENGTH_MSG, DESC_MAX_LENGTH));

            RuleFor(x => x.HomepageUrl)
                .MaximumLength(35)
                .WithMessage(string.Format(URL_LENGTH_MSG, URL_MAX_LENGTH));

            RuleFor(x => x.TournamentFormatId)
                .NotEmpty();

            RuleFor(x => x.TeamImage)
                .NotEmpty()
                .SetValidator(new CustomGameImageFileValidator(fileValidate));
        }
    }
}
