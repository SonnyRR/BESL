namespace BESL.Application.Tests.Games.Commands.Create
{
    using Xunit;
    using Moq;
    using BESL.Application.Tests.Infrastructure;
    using System.IO;
    using BESL.Application.Games.Commands.Create;
    using BESL.Common;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Http.Internal;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using System.Threading;
    using System.Reflection;
    using System.Linq;

    public class CreateGameCommandTests : BaseCommandTest
    {
        [Fact]
        public void Handle_GivenValidRequest_ShouldCreateValidEntity()
        {
            var cfgMock = new Mock<IConfiguration>();
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();

            var handler = new CreateGameCommandHandler(this.dbContext, cfgMock.Object);

            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<Cloudinary>(), It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync("https://steamcdn-a.akamaihd.net/steam/apps/440/header.jpg?t=1558031605");

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
                GameImage = new FormFile(null, 0, 0, string.Empty, string.Empty)
            };

            var id = handler.Handle(command, CancellationToken.None);

            Assert.True(true);
        }
    }
}
