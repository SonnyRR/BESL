namespace BESL.Application.Tests.Games.Commands.Create
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Moq;
    using Microsoft.AspNetCore.Http;
    using Shouldly;
    using Xunit;

    using BESL.Application.Games.Commands.Create;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Entities;
    using BESL.Application.Exceptions;

    public class CreateGameCommandTests : BaseTest<Game>
    {
        [Trait(nameof(Game), "CreateGame command tests.")]
        [Fact(DisplayName ="Handle given valid request should create valid entity.")]
        public async Task Handle_GivenValidRequest_ShouldCreateValidEntity()
        {
            // Arrange
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            var cloudinaryMock = new Mock<Cloudinary>();
            var imagePlaceholderUrl = "https://steamcdn-a.akamaihd.net/steam/apps/440/header.jpg";

            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync(imagePlaceholderUrl);

            var sut = new CreateGameCommandHandler(this.deletableEntityRepository, cloudinaryHelperMock.Object, this.mediatorMock.Object);

            var command = new CreateGameCommand()
            {
                Name = "Team Fortress 2",
                Description = @"One of the most popular online action games of all time, Team Fortress 2 delivers constant free updates—new game modes, maps, equipment and, most importantly, hats. Nine distinct classes provide a broad range of tactical abilities and personalities, and lend themselves to a variety of player skills. New to TF ? Don’t sweat it! No matter what your style and experience, we’ve got a character for you.Detailed training and offline practice modes will help you hone your skills before jumping into one of TF2’s many game modes, including Capture the Flag, Control Point, Payload, Arena, King of the Hill and more. Make a character your own! There are hundreds of weapons, hats and more to collect, craft, buy and trade.Tweak your favorite class to suit your gameplay style and personal taste.You don’t need to pay to win—virtually all of the items in the Mann Co.Store can also be found in-game.",
                GameImage = new FormFile(It.IsAny<Stream>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>())
            };

            // Act
            await sut.Handle(command, CancellationToken.None);
            var game = this.dbContext.Games.SingleOrDefault(g => g.Name == command.Name);

            // Assert
            this.mediatorMock.Verify(x => x.Publish(It.IsAny<GameCreatedNotification>(), It.IsAny<CancellationToken>()));
            game.ShouldNotBeNull();            
            game.Name.ShouldBe(command.Name);
            game.GameImageUrl.ShouldBe(imagePlaceholderUrl);
            game.Description.ShouldBe(command.Description);
        }

        [Trait(nameof(Game), "CreateGame command tests.")]
        [Fact(DisplayName ="Handle given null request should throw ArgumentNullException.")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateGameCommandHandler(It.IsAny<IDeletableEntityRepository<Game>>(), It.IsAny<ICloudinaryHelper>(), this.mediatorMock.Object);
            CreateGameCommand command = null;

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Game), "CreateGame command tests.")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException.")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var sut = new CreateGameCommandHandler(this.deletableEntityRepository, It.IsAny<ICloudinaryHelper>(), this.mediatorMock.Object);
            CreateGameCommand command = new CreateGameCommand { Name = "SampleGame1" };

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}