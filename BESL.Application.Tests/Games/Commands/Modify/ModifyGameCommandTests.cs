namespace BESL.Application.Tests.Games.Commands.Modify
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

    using BESL.Application.Tests.Infrastructure;
    using BESL.Common;
    using System;
    using System.IO;
    using BESL.Application.Games.Commands.Modify;
    using BESL.Domain.Entities;
    using BESL.Application.Infrastructure.Validators;

    public class ModifyGameCommandTests : BaseTest
    {
        [Fact]
        public void Handle_GivenValidRequest_ShouldModifyEntity()
        {
            // Arrange
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            var sut = new ModifyGameCommandHandler(this.dbContext, It.IsAny<IConfiguration>());

            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<Cloudinary>(), It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync("https://steamcdn-a.akamaihd.net/steam/apps/440/changed-picture.jpg");

            cloudinaryHelperMock
                .Setup(x => x.GetInstance(It.IsAny<IConfiguration>()))
                .Returns(() => null);

            var field = typeof(ModifyGameCommandHandler)
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .SingleOrDefault(f => f.IsInitOnly && f.FieldType == typeof(ICloudinaryHelper));

            field.SetValue(sut, cloudinaryHelperMock.Object);

            var command = new ModifyGameCommand()
            {
                Id = this.CreateGame(),
                Name = "Team Fortress 3",
                Description = "Changed description from test",
                GameImage = new FormFile(It.IsAny<Stream>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>())
            };

            // Act
            var id = sut.Handle(command, CancellationToken.None).GetAwaiter().GetResult();
            var game = this.dbContext.Games.SingleOrDefault(g => g.Name == "Team Fortress 3");

            // Assert
            game.ShouldNotBeNull();
            game.Description.ShouldBe("Changed description from test");
            game.GameImageUrl.ShouldBe("https://steamcdn-a.akamaihd.net/steam/apps/440/changed-picture.jpg");
        }

        [Theory]
        [InlineData("invalid-file.txt", false, "text/plain", "Dota 2", "The most-played game on Steam. Every day, millions of players worldwide enter battle as one of over a hundred Dota heroes. And no matter if it's their 10th hour of play or 1,000th, there's always something new to discover. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has truly taken on a life of its own. One Battlefield. Infinite Possibilities. When it comes to diversity of heroes, abilities, and powerful items, Dota boasts an endless array—no two games are the same. Any hero can fill multiple roles, and there's an abundance of items to help meet the needs of each game. Dota doesn't provide limitations on how to play, it empowers you to express your own style.")]
        [InlineData("gamePictureValid.jpg", false, "image/jpeg", "D", "The most-played game on Steam. Every day, millions of players worldwide enter battle as one of over a hundred Dota heroes. And no matter if it's their 10th hour of play or 1,000th, there's always something new to discover. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has truly taken on a life of its own. One Battlefield. Infinite Possibilities. When it comes to diversity of heroes, abilities, and powerful items, Dota boasts an endless array—no two games are the same. Any hero can fill multiple roles, and there's an abundance of items to help meet the needs of each game. Dota doesn't provide limitations on how to play, it empowers you to express your own style.")] 
        [InlineData("gamePictureValid.jpg", true, "image/jpeg", "Dota 2", "The most-played game on Steam. Every day, millions of players worldwide enter battle as one of over a hundred Dota heroes. And no matter if it's their 10th hour of play or 1,000th, there's always something new to discover. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has truly taken on a life of its own. One Battlefield. Infinite Possibilities. When it comes to diversity of heroes, abilities, and powerful items, Dota boasts an endless array—no two games are the same. Any hero can fill multiple roles, and there's an abundance of items to help meet the needs of each game. Dota doesn't provide limitations on how to play, it empowers you to express your own style.")] 
        [InlineData("gamePictureValid.jpg", false, "image/jpeg", "Dota 2", "invalidDescription")]
        public void Validator_GivenValidRequest_ShouldValidateCorrectly(string fileName, bool validValue, string contentType, string gameName, string gameDesc)
        {
            // Arrange
            var fileStream = File.OpenRead(Path.Combine("Common", "TestPictures", fileName));
            var formFile = new FormFile(fileStream, 0, fileStream.Length, "gamePicture", fileName)
            {
                Headers = new HeaderDictionary()
            };
            formFile.ContentType = contentType;
            var request = new ModifyGameCommand()
            {
                Name = gameName,
                Description = gameDesc,
                GameImage = formFile
            };

            // Act
            var sut = new ModifyGameCommandValidator(new GameImageFileValidate());
            var validationResult = sut.Validate(request);

            // Assert
            validationResult.IsValid.ShouldBe(validValue);
        }

        private int CreateGame()
        {
            var game = new Game()
            {
                Name = "Team Fortress 2",
                Description = "Original Description",
                GameImageUrl = "https://steamcdn-a.akamaihd.net/steam/apps/440/header.jpg",
                CreatedOn = DateTime.UtcNow,
            };

            this.dbContext.Games.Add(game);
            this.dbContext.SaveChanges();

            return game.Id;
        }
    }
}
