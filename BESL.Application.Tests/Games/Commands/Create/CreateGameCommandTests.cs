namespace BESL.Application.Tests.Games.Commands.Create
{
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
    using BESL.Application.Tests.Infrastructure;
    using BESL.Common;
    using System;
    using BESL.Application.Interfaces;
    using System.IO;
    using BESL.Application.Infrastructure.Validators;

    public class CreateGameCommandTests : BaseCommandTest
    {
        [Fact]
        public void Handle_GivenValidRequest_ShouldCreateValidEntity()
        {
            // Arrange
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            var handler = new CreateGameCommandHandler(this.dbContext, It.IsAny<IConfiguration>());

            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<Cloudinary>(), It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync("https://steamcdn-a.akamaihd.net/steam/apps/440/header.jpg");

            cloudinaryHelperMock
                .Setup(x => x.GetInstance(It.IsAny<IConfiguration>()))
                .Returns(() => null);

            var field = typeof(CreateGameCommandHandler)
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .SingleOrDefault(f => f.IsInitOnly && f.FieldType == typeof(ICloudinaryHelper));

            field.SetValue(handler, cloudinaryHelperMock.Object);

            var command = new CreateGameCommand()
            {
                Name = "Team Fortress 2",
                Description = @"One of the most popular online action games of all time, Team Fortress 2 delivers constant free updates—new game modes, maps, equipment and, most importantly, hats. Nine distinct classes provide a broad range of tactical abilities and personalities, and lend themselves to a variety of player skills. New to TF ? Don’t sweat it! No matter what your style and experience, we’ve got a character for you.Detailed training and offline practice modes will help you hone your skills before jumping into one of TF2’s many game modes, including Capture the Flag, Control Point, Payload, Arena, King of the Hill and more. Make a character your own! There are hundreds of weapons, hats and more to collect, craft, buy and trade.Tweak your favorite class to suit your gameplay style and personal taste.You don’t need to pay to win—virtually all of the items in the Mann Co.Store can also be found in-game.",
                GameImage = new FormFile(It.IsAny<Stream>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>())
            };

            // Act
            var id = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();
            var game = this.dbContext.Games.SingleOrDefault(g => g.Id == id);

            // Assert
            game.ShouldNotBeNull();
            id.ShouldBe(1);
            this.dbContext.Games.Count().ShouldBe(1);
            game.Name.ShouldBe("Team Fortress 2");
            game.GameImageUrl.ShouldBe("https://steamcdn-a.akamaihd.net/steam/apps/440/header.jpg");
            game.Description.ShouldBe(@"One of the most popular online action games of all time, Team Fortress 2 delivers constant free updates—new game modes, maps, equipment and, most importantly, hats. Nine distinct classes provide a broad range of tactical abilities and personalities, and lend themselves to a variety of player skills. New to TF ? Don’t sweat it! No matter what your style and experience, we’ve got a character for you.Detailed training and offline practice modes will help you hone your skills before jumping into one of TF2’s many game modes, including Capture the Flag, Control Point, Payload, Arena, King of the Hill and more. Make a character your own! There are hundreds of weapons, hats and more to collect, craft, buy and trade.Tweak your favorite class to suit your gameplay style and personal taste.You don’t need to pay to win—virtually all of the items in the Mann Co.Store can also be found in-game.");
        }

        [Fact]
        public void Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var handler = new CreateGameCommandHandler(It.IsAny<IApplicationDbContext>(), It.IsAny<IConfiguration>());
            CreateGameCommand command = null;

            // Assert
            Should.Throw<ArgumentNullException>(() => handler.Handle(command, It.IsAny<CancellationToken>()).GetAwaiter().GetResult());
        }

        [Fact]
        public void Validator_GivenValidRequest_ShouldValidateCorrectly()
        {
            // Arrange
            var fileStream = File.OpenRead(Path.Combine("Common", "TestPictures", "gamePicureValid.jpg"));
            var formFile = new FormFile(fileStream, 0, fileStream.Length, "gamePicture", "gamePictureValid.jpg")
            {
                Headers = new HeaderDictionary()
            };
            formFile.ContentType = "image/jpeg";
            var request = new CreateGameCommand()
            {
                Name = "Dota 2",
                Description = "The most-played game on Steam. Every day, millions of players worldwide enter battle as one of over a hundred Dota heroes. And no matter if it's their 10th hour of play or 1,000th, there's always something new to discover. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has truly taken on a life of its own. One Battlefield. Infinite Possibilities. When it comes to diversity of heroes, abilities, and powerful items, Dota boasts an endless array—no two games are the same. Any hero can fill multiple roles, and there's an abundance of items to help meet the needs of each game. Dota doesn't provide limitations on how to play, it empowers you to express your own style.",
                GameImage = formFile
            };

            // Act
            var validator = new CreateGameCommandValidator(new GameImageFileValidate());
            var validationResult = validator.Validate(request);

            // Assert
            validationResult.IsValid.ShouldBe(true);
        }
    }
}