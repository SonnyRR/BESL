namespace BESL.Application.Matches.Commands.Create
{
    using System;
    using BESL.Application.Interfaces;
    using FluentValidation;
    using static Validation.Constants;

    public class CreateMatchCommandValidator : AbstractValidator<CreateMatchCommand>
    {
        public CreateMatchCommandValidator(IDateTime machineTime)
        {
            RuleFor(x => x.HomeTeamId)
                .NotEqual(x => x.AwayTeamId)
                .NotEmpty();

            RuleFor(x => x.AwayTeamId)
                .NotEqual(x => x.HomeTeamId)
                .NotEmpty();

            RuleFor(x => x.PlayDate)
                .NotEmpty()
                .Must(x => x.DayOfWeek != DayOfWeek.Sunday)
                .WithMessage(PLAY_DATE_CANNOT_BE_ON_SUNDAY)
                .Must(x => x.ToUniversalTime().Date >= machineTime.UtcNow.AddMinutes(-10).Date)
                .WithMessage(PLAY_DATE_CANNOT_BE_IN_THE_PAST);
        }
    }
}
