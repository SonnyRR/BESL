namespace BESL.Application.Tests.Games.Commands.Modify
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Games.Commands.Modify;
    using BESL.Application.Infrastructure.Validators;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;

    public class ModifyGameCommandTests : BaseTest<Game>
    {
        [Trait(nameof(Game), "Game modify tests.")]
        [Fact(DisplayName = "Handler given valid request should modify entity.")]
        public async Task Handle_GivenValidRequest_ShouldModifyEntity()
        {
            // Arrange
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();

            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync("https://steamcdn-a.akamaihd.net/steam/apps/440/changed-picture.jpg");

            var sut = new ModifyGameCommandHandler(deletableEntityRepository, cloudinaryHelperMock.Object);

            var gameId = 1;
            var command = new ModifyGameCommand()
            {
                Id = gameId,
                Name = "Team Fortress 3",
                Description = "Changed description from test",
                GameImage = new FormFile(It.IsAny<Stream>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>())
            };

            // Act
            var id = await sut.Handle(command, CancellationToken.None);
            var game = await deletableEntityRepository.GetByIdWithDeletedAsync(gameId);

            // Assert
            game.ShouldNotBeNull();
            game.Description.ShouldBe("Changed description from test");
            game.GameImageUrl.ShouldBe("https://steamcdn-a.akamaihd.net/steam/apps/440/changed-picture.jpg");
        }

        [Trait(nameof(Game), "Game modify tests.")]
        [Theory(DisplayName = "Validator given invalid requests should validate correctly.")]
        [InlineData("invalid-file.txt", "text/plain", "Dota 2", "The most-played game on Steam. Every day, millions of players worldwide enter battle as one of over a hundred Dota heroes. And no matter if it's their 10th hour of play or 1,000th, there's always something new to discover. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has truly taken on a life of its own. One Battlefield. Infinite Possibilities. When it comes to diversity of heroes, abilities, and powerful items, Dota boasts an endless array—no two games are the same. Any hero can fill multiple roles, and there's an abundance of items to help meet the needs of each game. Dota doesn't provide limitations on how to play, it empowers you to express your own style.")]
        [InlineData("gamePictureValid.jpg", "image/jpeg", "D", "The most-played game on Steam. Every day, millions of players worldwide enter battle as one of over a hundred Dota heroes. And no matter if it's their 10th hour of play or 1,000th, there's always something new to discover. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has truly taken on a life of its own. One Battlefield. Infinite Possibilities. When it comes to diversity of heroes, abilities, and powerful items, Dota boasts an endless array—no two games are the same. Any hero can fill multiple roles, and there's an abundance of items to help meet the needs of each game. Dota doesn't provide limitations on how to play, it empowers you to express your own style.")] 
        [InlineData("gamePictureValid.jpg", "image/jpeg", "Dota 2", "invalidDescription")]
        public void Validator_GivenInvalidRequests_ShouldValidateCorrectly(string fileName, string contentType, string gameName, string gameDesc)
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
            validationResult.IsValid.ShouldBe(false);
        }

        [Trait(nameof(Game), "Game modify tests.")]
        [Fact(DisplayName = "Validator given valid request should validate successfully")]
        public void Validator_GivenValidRequest_ShouldValidateCorrectly()
        {
            // Arrange
            var fileStream = File.OpenRead(Path.Combine("Common", "TestPictures", "gamePictureValid.jpg"));
            var formFile = new FormFile(fileStream, 0, fileStream.Length, "gamePicture", "gamePictureValid.jpg")
            {
                Headers = new HeaderDictionary()
            };

            formFile.ContentType = "image/jpeg";
            var request = new ModifyGameCommand()
            {
                Name = "Dota 2",
                Description = "The most-played game on Steam. Every day, millions of players worldwide enter battle as one of over a hundred Dota heroes. And no matter if it's their 10th hour of play or 1,000th, there's always something new to discover. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has truly taken on a life of its own. One Battlefield. Infinite Possibilities. When it comes to diversity of heroes, abilities, and powerful items, Dota boasts an endless array—no two games are the same. Any hero can fill multiple roles, and there's an abundance of items to help meet the needs of each game. Dota doesn't provide limitations on how to play, it empowers you to express your own style.",
                GameImage = formFile
            };

            // Act
            var sut = new ModifyGameCommandValidator(new GameImageFileValidate());
            var validationResult = sut.Validate(request);

            // Assert
            validationResult.IsValid.ShouldBe(true);
        }

        [Trait(nameof(Game), "Game modify tests.")]
        [Fact(DisplayName = "Validator given valid request with no image should validate successfully")]
        public void Validator_GivenValidRequestWithNoImage_ShouldValidateCorrectly()
        {
            // Arrange
            var request = new ModifyGameCommand()
            {
                Name = "Dota 2",
                Description = "The most-played game on Steam. Every day, millions of players worldwide enter battle as one of over a hundred Dota heroes. And no matter if it's their 10th hour of play or 1,000th, there's always something new to discover. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has truly taken on a life of its own. One Battlefield. Infinite Possibilities. When it comes to diversity of heroes, abilities, and powerful items, Dota boasts an endless array—no two games are the same. Any hero can fill multiple roles, and there's an abundance of items to help meet the needs of each game. Dota doesn't provide limitations on how to play, it empowers you to express your own style.",
                GameImage = null
            };

            // Act
            var sut = new ModifyGameCommandValidator(new GameImageFileValidate());
            var validationResult = sut.Validate(request);

            // Assert
            validationResult.IsValid.ShouldBe(true);
        }
    }
}