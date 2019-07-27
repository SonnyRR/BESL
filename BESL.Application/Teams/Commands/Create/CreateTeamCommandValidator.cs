namespace BESL.Application.Teams.Commands.Create
{
    using FluentValidation;

    using BESL.Application.Infrastructure.Validators;
    using BESL.Application.Interfaces;
    using static BESL.Application.Teams.Commands.Validation.Constants;

    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
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
