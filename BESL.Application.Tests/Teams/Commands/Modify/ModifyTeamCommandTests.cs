namespace BESL.Application.Tests.Teams.Commands.Modify
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Commands.Modify;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Entities;

    public class ModifyTeamCommandTests : BaseTest<Team>
    {
        [Trait(nameof(Team), "ModifyTeam command tests")]
        [Fact(DisplayName = "Handle given valid request should modify team")]
        public async Task Handle_GivenValidRequest_ShouldModifyTeam()
        {
            // Arrange
            var command = new ModifyTeamCommand
            {
                Id = 1,
                Name = "ModifiedName",
                Description = "ModifiedDescription",
                HomepageUrl = "ModifiedHomepage",
                TeamImage = new Mock<IFormFile>().Object
            };

            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync("http://test.bg/1.jpg");

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new ModifyTeamCommandHandler(this.deletableEntityRepository, cloudinaryHelperMock.Object, userAccessorMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            var modifiedTeam = this.dbContext.Teams.SingleOrDefault(x => x.Id == id);

            modifiedTeam.ShouldNotBeNull();
            modifiedTeam.Name.ShouldBe("ModifiedName");
            modifiedTeam.Description.ShouldBe("ModifiedDescription");
            modifiedTeam.HomepageUrl.ShouldBe("ModifiedHomepage");
            modifiedTeam.ImageUrl.ShouldBe("http://test.bg/1.jpg");
        }

        [Trait(nameof(Team), "ModifyTeam command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new ModifyTeamCommandHandler(It.IsAny<IDeletableEntityRepository<Team>>(), It.IsAny<ICloudinaryHelper>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "ModifyTeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new ModifyTeamCommand
            {
                Id = 311,
            };

            var sut = new ModifyTeamCommandHandler(this.deletableEntityRepository, It.IsAny<ICloudinaryHelper>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "ModifyTeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ForbiddenException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowForbiddenException()
        {
            // Arrange
            var command = new ModifyTeamCommand
            {
                Id = 1
            };

            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync("http://test.bg/1.jpg");

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("InvalidId");

            var sut = new ModifyTeamCommandHandler(this.deletableEntityRepository, cloudinaryHelperMock.Object, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<ForbiddenException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "ModifyTeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new ModifyTeamCommand
            {
                Id = 1,
                Name = "FooTeam2",
                Description = "ModifiedDescription",
                HomepageUrl = "ModifiedHomepage",
                TeamImage = new Mock<IFormFile>().Object
            };

            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync("http://test.bg/1.jpg");

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new ModifyTeamCommandHandler(this.deletableEntityRepository, cloudinaryHelperMock.Object, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

    }
}
