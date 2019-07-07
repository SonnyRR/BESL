namespace BESL.Application.Games.Queries.ModifyGame
{
    using BESL.Application.Infrastructure.Validators;
    using BESL.Application.Interfaces;
    using FluentValidation;

    public class ModifyGameViewModelValidator : AbstractValidator<ModifyGameViewModel>
    {
        private const int NAME_MIN_LENGTH = 3, NAME_MAX_LENGTH = 40;
        private const int DESC_MIN_LENGTH = 160, DESC_MAX_LENGTH = 1000;

        private const string NAME_LENGTH_MSG = "Game name length must be between {0} and {1} characters long!";
        private const string DESC_LENGTH_MSG = "Game description length must be between {0} and {1} characters long!";

        public ModifyGameViewModelValidator(IFileValidate fileValidate)
        {
            RuleFor(x => x.Name)
                .Length(NAME_MIN_LENGTH, NAME_MAX_LENGTH)
                .NotEmpty()
                .WithMessage(string.Format(NAME_LENGTH_MSG, NAME_MIN_LENGTH, NAME_MAX_LENGTH));

            RuleFor(x => x.Description)
                .Length(DESC_MIN_LENGTH, DESC_MAX_LENGTH)
                .NotEmpty()
                .WithMessage(string.Format(DESC_LENGTH_MSG, DESC_MIN_LENGTH, DESC_MAX_LENGTH));

            RuleFor(x => x.GameImage)
                .NotEmpty()
                .NotNull()
                .SetValidator(new CustomGameImageFileValidator(fileValidate));
        }
    }
}
