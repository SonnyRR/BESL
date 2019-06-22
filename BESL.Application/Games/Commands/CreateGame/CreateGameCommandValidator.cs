namespace BESL.Application.Games.Commands.CreateGame
{
    using FluentValidation;

    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(x => x.Name).Length(3, 40);
            RuleFor(x => x.Description).Length(20, 1000).NotEmpty();
        }
    }
}
