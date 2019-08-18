namespace BESL.Application.Tests.Games.Commands.Modify
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using MediatR;
    using Moq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Games.Commands.Modify;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;

    public class ModifyGameCommandTests : BaseTest<Game>
    {
        private string imagePlaceholderUrl = "https://steamcdn-a.akamaihd.net/steam/apps/440/changed-picture.jpg";

        [Trait(nameof(Game), "ModifyGame command tests.")]
        [Fact(DisplayName = "Handler given valid request should modify entity.")]
        public async Task Handle_GivenValidRequest_ShouldModifyEntity()
        {
            // Arrange
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();

            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync(imagePlaceholderUrl);

            var sut = new ModifyGameCommandHandler(deletableEntityRepository, cloudinaryHelperMock.Object, this.mediatorMock.Object);

            var gameId = 2;
            var command = new ModifyGameCommand()
            {
                Id = gameId,
                Name = "Team Fortress 3",
                Description = "Changed description from test",
                GameImage = new FormFile(It.IsAny<Stream>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>())
            };

            // Act
            var id = await sut.Handle(command, CancellationToken.None);

            // Assert
            var game = this.deletableEntityRepository
                .AllAsNoTrackingWithDeleted()
                .SingleOrDefault(x => x.Id == gameId);

            id.ShouldBe(2);
            this.mediatorMock.Verify(x => x.Publish(It.IsAny<GameModifiedNotification>(), It.IsAny<CancellationToken>()));
            game.ShouldNotBeNull();
            game.Name.ShouldBe(command.Name);
            game.Description.ShouldBe(command.Description);
            game.GameImageUrl.ShouldBe(imagePlaceholderUrl);
        }

        [Trait(nameof(Game), "ModifyGame command tests.")]
        [Fact(DisplayName = "Handler given invalid request should throw NotFoundException.")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();

            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync(this.imagePlaceholderUrl);

            var sut = new ModifyGameCommandHandler(deletableEntityRepository, cloudinaryHelperMock.Object, this.mediatorMock.Object);

            var gameId = 41241;
            var command = new ModifyGameCommand()
            {
                Id = gameId,
                Name = It.IsAny<string>(),
                Description = It.IsAny<string>(),
                GameImage = new FormFile(It.IsAny<Stream>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>())
            };

            // Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Game), "ModifyGame command tests.")]
        [Fact(DisplayName = "Handler given null request should throw ArgumentNullException.")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Act
            var sut = new ModifyGameCommandHandler(It.IsAny<IDeletableEntityRepository<Game>>(), It.IsAny<ICloudinaryHelper>(), It.IsAny<IMediator>());

            // Act
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}