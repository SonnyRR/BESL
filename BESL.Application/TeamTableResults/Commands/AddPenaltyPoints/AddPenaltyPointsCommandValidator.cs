namespace BESL.Application.TeamTableResults.Commands.AddPenaltyPoints
{
    using FluentValidation;
    using static BESL.SharedKernel.GlobalConstants;

    public class AddPenaltyPointsCommandValidator : AbstractValidator<AddPenaltyPointsCommand>
    {
        public AddPenaltyPointsCommandValidator()
        {
            RuleFor(x => x.TeamTableResultId)
                .NotEmpty();

            RuleFor(x => x.PenaltyPoints)
                .InclusiveBetween(MIN_PENALTY_POINTS, MAX_PENALTY_POINTS);
        }
    }
}
