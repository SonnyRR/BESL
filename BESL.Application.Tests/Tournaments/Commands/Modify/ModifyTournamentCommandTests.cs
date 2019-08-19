namespace BESL.Application.Tests.Tournaments.Commands.Modify
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
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Tournaments.Commands.Modify;
    using BESL.Domain.Entities;

    public class ModifyTournamentCommandTests : BaseTest<Tournament>
    {
        [Trait(nameof(Tournament), "ModifyTournament command tests")]
        [Fact(DisplayName ="Handle given valid request should modify entity")]
        public async Task Handle_GivenValidRequest_ShouldModifyEntity()
        {
            // Arrange
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            var cloudinaryMock = new Mock<Cloudinary>();
            var imagePlaceholderUrl = "https://test.bg/1.jpg";

            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync(imagePlaceholderUrl);

            var command = new ModifyTournamentCommand
            {
                Id = 2,
                Name = "EditedName",
                Description = "EditedDescription",
                StartDate = new DateTime(2019, 09, 02),
                EndDate = new DateTime(2019, 09, 30),
                AreSignupsOpen = true,
                IsActive = false
            };

            var sut = new ModifyTournamentCommandHandler(this.deletableEntityRepository, cloudinaryHelperMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldBeGreaterThan(0);

            var modifiedTournament = this.deletableEntityRepository.AllAsNoTracking().SingleOrDefault(x => x.Id == id);

            modifiedTournament.Name.ShouldBe("EditedName");
            modifiedTournament.Description.ShouldBe("EditedDescription");
            modifiedTournament.StartDate.ShouldBe(new DateTime(2019, 09, 02));
            modifiedTournament.EndDate.ShouldBe(new DateTime(2019, 09, 30));
            modifiedTournament.AreSignupsOpen.ShouldBe(true);
            modifiedTournament.IsActive.ShouldBe(false);
        }

        [Trait(nameof(Tournament), "ModifyTournament command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new ModifyTournamentCommandHandler(It.IsAny<IDeletableEntityRepository<Tournament>>(), It.IsAny<ICloudinaryHelper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "ModifyTournament command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new ModifyTournamentCommand { Id = 122142 };
            var sut = new ModifyTournamentCommandHandler(this.deletableEntityRepository, It.IsAny<ICloudinaryHelper>());

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "ModifyTournament command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new ModifyTournamentCommand { Id = 1, Name = "TestTournament2" };
            var sut = new ModifyTournamentCommandHandler(this.deletableEntityRepository, It.IsAny<ICloudinaryHelper>());

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
