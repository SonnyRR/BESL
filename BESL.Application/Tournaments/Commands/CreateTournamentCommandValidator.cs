﻿namespace BESL.Application.Tournaments.Commands
{
    using FluentValidation;

    using BESL.Application.Interfaces;
    using BESL.Application.Infrastructure.Validators;

    public class CreateTournamentCommandValidator : AbstractValidator<CreateTournamentCommand>
    {
        private const int NAME_MIN_LENGTH = 3, NAME_MAX_LENGTH = 35;
        private const int DESC_MIN_LENGTH = 0, DESC_MAX_LENGTH = 200;

        private const string NAME_LENGTH_MSG = "Tournament name length must be between {0} and {1} characters long!";
        private const string DESC_LENGTH_MSG = "Tournament description length must be between {0} and {1} characters long!";

        public CreateTournamentCommandValidator(IFileValidate fileValidate)
        {
            RuleFor(x => x.Name)
                .Length(NAME_MIN_LENGTH, NAME_MAX_LENGTH)
                .WithMessage(string.Format(NAME_LENGTH_MSG, NAME_MIN_LENGTH, NAME_MAX_LENGTH));

            RuleFor(x => x.Description)
                .Length(DESC_MIN_LENGTH, DESC_MAX_LENGTH)
                .WithMessage(string.Format(DESC_LENGTH_MSG, DESC_MIN_LENGTH, DESC_MAX_LENGTH));

            RuleFor(x => x.GameImage)
                .NotEmpty()
                .NotNull()
                .SetValidator(new CustomGameImageFileValidator(fileValidate));
        }
    }
}
