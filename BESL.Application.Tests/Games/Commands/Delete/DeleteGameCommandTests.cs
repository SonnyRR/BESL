namespace BESL.Application.Tests.Games.Commands.Delete
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Games.Commands.Create;
    using BESL.Application.Games.Commands.Delete;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Common.Cloudinary;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;
    using BESL.Application.Interfaces;
    using BESL.Persistence.Repositories;
    using System.Threading.Tasks;

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
        public void Handle_GivenInvalidRequest_ShouldThrowCorrectExceptions(int id, Type exceptionType)
        {
            // Arrange
            if (exceptionType == typeof(DeleteFailureException))
            {
                id = this.CreateSampleGame();
                this.dbContext
                    .Games
                    .SingleOrDefault(g => g.Id == id)
                    .IsDeleted = true;
                this.dbContext.SaveChanges();
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

        private int CreateSampleGame()
        {
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            var sut = new CreateGameCommandHandler(this.repository, It.IsAny<IConfiguration>());

            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<Cloudinary>(), It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync("https://steamcdn-a.akamaihd.net/steam/apps/440/header.jpg");

            cloudinaryHelperMock
                .Setup(x => x.GetInstance(It.IsAny<IConfiguration>()))
                .Returns(() => null);

            var field = typeof(CreateGameCommandHandler)
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .SingleOrDefault(f => f.IsInitOnly && f.FieldType == typeof(ICloudinaryHelper));

            field.SetValue(sut, cloudinaryHelperMock.Object);
            var gameCommandRequest = new CreateGameCommand()
            {
                Name = "Team Fortress 2",
                Description = "Stupid hat simulator.",
                GameImage = new FormFile(It.IsAny<Stream>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>())
            };

            var id = sut.Handle(gameCommandRequest, It.IsAny<CancellationToken>()).GetAwaiter().GetResult();
            return id;
        }
    }
}
