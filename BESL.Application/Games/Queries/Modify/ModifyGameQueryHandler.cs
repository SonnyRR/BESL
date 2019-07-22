namespace BESL.Application.Games.Queries.Modify

{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;

    public class ModifyGameQueryHandler : IRequestHandler<ModifyGameQuery, ModifyGameViewModel>
    {
        private readonly IDeletableEntityRepository<Game> repository;

        public ModifyGameQueryHandler(IDeletableEntityRepository<Game> repository)
        {
            this.repository = repository;
        }

        public async Task<ModifyGameViewModel> Handle(ModifyGameQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var desiredGame = await this.repository
                .GetByIdWithDeletedAsync(request.Id);

            if (desiredGame == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            var viewModel = new ModifyGameViewModel()
            {
                Id = desiredGame.Id,
                Name = desiredGame.Name,
                Description = desiredGame.Description,
                GameImageUrl = desiredGame.GameImageUrl
            };

            return viewModel;
        }
    }
}
