namespace BESL.Application.Tests.Games.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using BESL.Application.Exceptions;
    using BESL.Application.Games.Commands.Delete;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class DeleteGameCommandTests : BaseTest<Game>
    {

        [Trait(nameof(Game), "Game deletion tests.")]
        [Fact(DisplayName = "Handler should mark entity as deleted.")]

        public async Task Handle_GivenValidRequest_ShouldMarkEntityAsDeleted()
        {
            // Arrange
            var deleteGameCommand = new DeleteGameCommand()
            {
                Id = 1,
                GameName = It.IsAny<string>()
            };

            var sut = new DeleteGameCommandHandler(deletableEntityRepository);
            
            // Act
            sut.Handle(deleteGameCommand, It.IsAny<CancellationToken>()).GetAwaiter().GetResult();

            // Assert
            var deletedGameEntity = await deletableEntityRepository.GetByIdWithDeletedAsync(1);
            deletedGameEntity.IsDeleted.ShouldBe(true);
        }

        [Trait(nameof(Game), "Game deletion tests.")]
        [Theory(DisplayName = "Handler should throw correct exceptions.")]
        [InlineData(90125, typeof(NotFoundException))]
        [InlineData(1, typeof(DeleteFailureException))]
        public async Task Handle_GivenInvalidRequest_ShouldThrowCorrectExceptions(int id, Type exceptionType)
        {
            // Arrange
            if (exceptionType == typeof(DeleteFailureException))
            {
                var game = await this.deletableEntityRepository.GetByIdWithDeletedAsync(id);
                deletableEntityRepository.Delete(game);
                await deletableEntityRepository.SaveChangesAsync();
            }

            var sut = new DeleteGameCommandHandler(this.deletableEntityRepository);
            var command = new DeleteGameCommand()
            {
                Id = id,
                GameName = It.IsAny<string>()
            };

            // Assert
            Should.Throw(() => sut.Handle(command, It.IsAny<CancellationToken>()).GetAwaiter().GetResult(), exceptionType);
        }

        [Trait(nameof(Game), "Game deletion tests.")]
        [Fact(DisplayName = "Handler should throw ArgumentNullException when request is null")]
        public void Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteGameCommandHandler(this.deletableEntityRepository);

            // Assert
            Should.Throw<ArgumentNullException>(() => sut.Handle(null, It.IsAny<CancellationToken>()).GetAwaiter().GetResult());
        }
    }
}
