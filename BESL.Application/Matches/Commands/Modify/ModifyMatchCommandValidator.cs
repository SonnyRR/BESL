namespace BESL.Application.Matches.Commands.Modify
{
    using System;
    using FluentValidation;
    using static Validation.Constants;

    public class ModifyMatchCommandValidator : AbstractValidator<ModifyMatchCommand>
    {
        public ModifyMatchCommandValidator()
        {
            When(x => x.AwayTeamScore.HasValue, () => RuleFor(x => x.AwayTeamScore)
                  .GreaterThanOrEqualTo(MATCH_MIN_POINTS)
                  .LessThanOrEqualTo(MATCH_MAX_POINTS));

            When(x => x.HomeTeamScore.HasValue, () => RuleFor(x => x.HomeTeamScore)
                  .GreaterThanOrEqualTo(MATCH_MIN_POINTS)
                  .LessThanOrEqualTo(MATCH_MAX_POINTS));

            RuleFor(x => x.ScheduledDate)
                .NotEmpty()
                .Must(x => x.DayOfWeek != DayOfWeek.Sunday)
                .WithMessage(PLAY_DATE_CANNOT_BE_ON_SUNDAY);
                //.Must(x => x.ToUniversalTime() > DateTime.UtcNow.AddMinutes(-10))
                //.WithMessage(PLAY_DATE_CANNOT_BE_IN_THE_PAST);
        }
    }
}
