namespace BESL.Application.Tournaments.Commands.Delete
{
    using FluentValidation;

    public class DeleteTournamentCommandValidator : AbstractValidator<DeleteTournamentCommand>
    {
        public DeleteTournamentCommandValidator()
        {
            RuleFor(cmd => cmd.Id)
                .NotEmpty();
        }
    }
}
