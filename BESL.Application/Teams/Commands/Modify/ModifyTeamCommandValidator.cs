namespace BESL.Application.Teams.Commands.Modify
{
    using FluentValidation;

    using BESL.Application.Infrastructure.Validators;
    using BESL.Application.Interfaces;
    using static BESL.Application.Teams.Commands.Validation.Constants;

    public class ModifyTeamCommandValidator : AbstractValidator<ModifyTeamCommand>
    {
        public ModifyTeamCommandValidator(IFileValidate fileValidate)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(NAME_MIN_LENGTH, NAME_MAX_LENGTH)
                .WithMessage(string.Format(NAME_LENGTH_MSG, NAME_MIN_LENGTH, NAME_MAX_LENGTH));

            RuleFor(x => x.Description)
                .Length(DESC_MIN_LENGTH, DESC_MAX_LENGTH)
                .WithMessage(string.Format(DESC_LENGTH_MSG, DESC_MIN_LENGTH, DESC_MAX_LENGTH));

            RuleFor(x => x.HomepageUrl)
                .Matches(URL_EXPRESSION)
                .WithMessage(URL_EXPRESSION_MSG);

            When(e => e.TeamImage != null, () => RuleFor(t => t.TeamImage)
                .SetValidator(new CustomGameImageFileValidator(fileValidate)));
        }
    }
}
