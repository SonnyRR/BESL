namespace BESL.Application.Games.Commands.Modify
{
    using FluentValidation;

    using BESL.Application.Infrastructure.Validators;
    using BESL.Application.Interfaces;
    using static Validation.Constants;

    public class ModifyGameCommandValidator : AbstractValidator<ModifyGameCommand>
    {
        public ModifyGameCommandValidator(IFileValidate fileValidate)
        {
            RuleFor(x => x.Name)
                .Length(NAME_MIN_LENGTH, NAME_MAX_LENGTH)
                .NotEmpty()
                .WithMessage(string.Format(NAME_LENGTH_MSG, NAME_MIN_LENGTH, NAME_MAX_LENGTH));

            RuleFor(x => x.Description)
                .Length(DESC_MIN_LENGTH, DESC_MAX_LENGTH)
                .NotEmpty()
                .WithMessage(string.Format(DESC_LENGTH_MSG, DESC_MIN_LENGTH, DESC_MAX_LENGTH));

            When(g => g.GameImage != null, () => RuleFor(x => x.GameImage).SetValidator(new CustomGameImageFileValidator(fileValidate)));
        }
    }
}