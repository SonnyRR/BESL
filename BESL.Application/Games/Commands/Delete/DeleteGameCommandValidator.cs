namespace BESL.Application.Games.Commands.Delete
{
    using FluentValidation;

    public class DeleteGameCommandValidator : AbstractValidator<DeleteGameCommand>
    {
        public DeleteGameCommandValidator()
        {
            RuleFor(cmd => cmd.Id)
                   .NotEmpty();
        }
    }
}
