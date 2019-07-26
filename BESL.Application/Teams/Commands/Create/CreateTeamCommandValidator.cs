namespace BESL.Application.Teams.Commands.Create
{
    using BESL.Application.Infrastructure.Validators;
    using BESL.Application.Interfaces;
    using FluentValidation;

    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        private const int NAME_MIN_LENGTH = 3, NAME_MAX_LENGTH = 35;
        private const int DESC_MIN_LENGTH = 20, DESC_MAX_LENGTH = 1000;
        private const string URL_EXPRESSION = @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";

        private const string NAME_LENGTH_MSG = "Team name length must be between {0} and {1} characters long!";
        private const string DESC_LENGTH_MSG = "Team description length must be between {0} and {1} characters long!";
        private const string URL_EXPRESSION_MSG = "Homepage input value is not a valid URL!";

        public CreateTeamCommandValidator(IFileValidate fileValidate)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(NAME_MIN_LENGTH, NAME_MAX_LENGTH)
                .WithMessage(string.Format(NAME_LENGTH_MSG, NAME_MIN_LENGTH, NAME_MAX_LENGTH));

            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(DESC_MIN_LENGTH, DESC_MAX_LENGTH)
                .WithMessage(string.Format(DESC_LENGTH_MSG, DESC_MIN_LENGTH, DESC_MAX_LENGTH));

            RuleFor(x => x.HomepageUrl)
                .Matches(URL_EXPRESSION)
                .WithMessage(URL_EXPRESSION_MSG);

            RuleFor(x => x.TournamentFormatId)
                .NotEmpty();

            RuleFor(x => x.OwnerId)
                .NotEmpty();

            RuleFor(x => x.TeamImage)
                .NotEmpty()
                .SetValidator(new CustomGameImageFileValidator(fileValidate));
        }
    }
}
