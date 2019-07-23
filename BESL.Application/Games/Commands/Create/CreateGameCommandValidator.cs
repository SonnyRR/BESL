namespace BESL.Application.Games.Commands.Create
{
    using FluentValidation;

    using BESL.Application.Infrastructure.Validators;
    using BESL.Application.Interfaces;
    using static Validation.Constants;

    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {

        public CreateGameCommandValidator(IFileValidate fileValidate)
        {
            RuleFor(x => x.Name)
                .Length(NAME_MIN_LENGTH, NAME_MAX_LENGTH)
                .NotEmpty()
                .WithMessage(string.Format(NAME_LENGTH_MSG, NAME_MIN_LENGTH, NAME_MAX_LENGTH));

            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(DESC_MIN_LENGTH, DESC_MAX_LENGTH)
                .WithMessage(string.Format(DESC_LENGTH_MSG, DESC_MIN_LENGTH, DESC_MAX_LENGTH));

            RuleFor(x => x.GameImage)
                .NotEmpty()
                .SetValidator(new CustomGameImageFileValidator(fileValidate));
        }
    }
}