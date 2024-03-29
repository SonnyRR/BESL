﻿namespace BESL.Application.Tournaments.Commands.Create
{
    using System;

    using FluentValidation;

    using BESL.Application.Infrastructure.Validators;
    using BESL.Application.Interfaces;
    using static Validation.Constants;

    public class CreateTournamentCommandValidator : AbstractValidator<CreateTournamentCommand>
    {
        public CreateTournamentCommandValidator(IFileValidate fileValidate, IDateTime machineTime)
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
                .NotEmpty()
                .Must(t => t.DayOfWeek == DayOfWeek.Monday)
                .WithMessage(START_DATE_MUST_BE_MONDAY_MSG)
                .GreaterThanOrEqualTo(t => machineTime.UtcNow.Date)
                .WithMessage(START_DATE_MSG);

            RuleFor(t => t.EndDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(t => t.StartDate.AddMonths(END_DATE_MIN_MONTH_LENGTH).AddDays(END_DATE_MAX_DAYS_DIFF))
                .WithMessage(string.Format(END_DATE_MSG, END_DATE_MIN_MONTH_LENGTH))
                .Must(t => t.DayOfWeek == DayOfWeek.Sunday)
                .WithMessage(END_DATE_MUST_BE_SUNDAY_MSG);

            RuleFor(t => t.TournamentImage)
                .NotEmpty()
                .SetValidator(new CustomGameImageFileValidator(fileValidate));
        }
    }
}
