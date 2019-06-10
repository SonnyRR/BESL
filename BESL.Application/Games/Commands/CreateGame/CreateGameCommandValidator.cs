using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BESL.Application.Games.Commands.CreateGame
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(x => x.Name).Length(2, 25).NotEmpty();
        }
    }
}
