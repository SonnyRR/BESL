﻿namespace BESL.Application.Tests.Games.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Games.Commands.Delete;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;

    public class DeleteGameCommandTests : BaseTest<Game>
    {

        [Trait(nameof(Game), "Game deletion tests.")]
        [Fact(DisplayName = "Handler given valid request should mark entity as deleted.")]

        public async Task Handle_GivenValidRequest_ShouldMarkEntityAsDeleted()
        {
            // Arrange
            var gameId = 1;
            var deleteGameCommand = new DeleteGameCommand()
            {
                Id = gameId,
                GameName = It.IsAny<string>()
            };

            var sut = new DeleteGameCommandHandler(deletableEntityRepository);
            
            // Act
            await sut.Handle(deleteGameCommand, It.IsAny<CancellationToken>());

            // Assert
            var deletedGameEntity = await deletableEntityRepository.GetByIdWithDeletedAsync(gameId);
            deletedGameEntity.IsDeleted.ShouldBe(true);
        }

        [Trait(nameof(Game), "Game deletion tests.")]
        [Fact(DisplayName = "Handler given invalid request should throw NotFoundException.")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange            
            var sut = new DeleteGameCommandHandler(this.deletableEntityRepository);
            var command = new DeleteGameCommand()
            {
                Id = 90125,
                GameName = It.IsAny<string>()
            };

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Game), "Game deletion tests.")]
        [Fact(DisplayName = "Handler given valid request should throw ArgumentNullException")]
        public async Task Handle_GivenValidRequest_ShouldThrowDeleteFailureException()
        {
            // Arrange
            var sut = new DeleteGameCommandHandler(this.deletableEntityRepository);
            var desiredEntity = await this.deletableEntityRepository.GetByIdWithDeletedAsync(2);
            this.deletableEntityRepository.Delete(desiredEntity);
            await this.deletableEntityRepository.SaveChangesAsync();
            var command = new DeleteGameCommand()
            {
                Id = 2,
                GameName = It.IsAny<string>()
            };

            // Act & Assert
            await Should.ThrowAsync<DeleteFailureException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Game), "Game deletion tests.")]
        [Fact(DisplayName = "Handler given null request should throw ArgumentNullException")]
        public void Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteGameCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
