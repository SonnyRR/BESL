namespace BESL.Application.Tests.Tournaments.Commands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using MediatR;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Tournaments.Commands.Create;
    using BESL.Persistence.Repositories;
    using BESL.Domain.Entities;

    public class CreateTournamentCommandTests : BaseTest<Tournament>
    {
        [Trait(nameof(Tournament), "CreateTournament command tests")]
        [Fact(DisplayName = "Handle given valid request should create entity")]
        public async Task Handle_GivenValidRequest_ShouldCreateEntity()
        {
            // Arrange
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            var cloudinaryMock = new Mock<Cloudinary>();
            var imagePlaceholderUrl = "https://steamcdn-a.akamaihd.net/steam/apps/440/header.jpg";

            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync(imagePlaceholderUrl);

            var tournamentFormatsRepository = new EfDeletableEntityRepository<TournamentFormat>(this.dbContext);

            var command = new CreateTournamentCommand
            {
                Name = "ValidTournament",
                Description = "ValidDescription",
                StartDate = new DateTime(2019, 08, 12),
                EndDate = new DateTime(2019, 09, 08),
                FormatId = 1,
                TournamentImage = It.IsAny<IFormFile>()
            };

            var sut = new CreateTournamentCommandHandler(this.deletableEntityRepository, tournamentFormatsRepository, cloudinaryHelperMock.Object, this.mediatorMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldBeGreaterThan(0);

            var createdTournament = this.deletableEntityRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            createdTournament.Name.ShouldBe("ValidTournament");
            createdTournament.Description.ShouldBe("ValidDescription");
            createdTournament.EndDate.Month.ShouldBe(9);
        }

        [Trait(nameof(Tournament), "CreateTournament command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateTournamentCommandHandler(It.IsAny<IDeletableEntityRepository<Tournament>>(), It.IsAny<IDeletableEntityRepository<TournamentFormat>>(), It.IsAny<ICloudinaryHelper>(), It.IsAny<IMediator>());

            // Act & Arrange
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "CreateTournament command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new CreateTournamentCommand
            {
                Name = "TestTournament1",
                StartDate = new DateTime(2019, 08, 12),
            };

            var sut = new CreateTournamentCommandHandler(this.deletableEntityRepository, It.IsAny<IDeletableEntityRepository<TournamentFormat>>(), It.IsAny<ICloudinaryHelper>(), It.IsAny<IMediator>());

            // Act  & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "CreateTournament command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new CreateTournamentCommand
            {
                FormatId = 131341,
                StartDate = new DateTime(2019, 08, 12),
            };

            var tournamentFormatsRepository = new EfDeletableEntityRepository<TournamentFormat>(this.dbContext);
            var sut = new CreateTournamentCommandHandler(this.deletableEntityRepository, tournamentFormatsRepository, It.IsAny<ICloudinaryHelper>(), It.IsAny<IMediator>());

            // Act  & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "CreateTournament command tests")]
        [Fact(DisplayName = "Handle given Invalid request should throw TournamentActiveDateMustStartOnMondayException")]
        public async Task Handle_GivenInvalidRequest_ShouldThroTournamentActiveDateMustStartOnMondayException()
        {
            // Arrange
            var command = new CreateTournamentCommand
            {
                StartDate = new DateTime(2019, 01, 01)
            };

            var sut = new CreateTournamentCommandHandler(It.IsAny<IDeletableEntityRepository<Tournament>>(), It.IsAny<IDeletableEntityRepository<TournamentFormat>>(), It.IsAny<ICloudinaryHelper>(), It.IsAny<IMediator>());

            // Act & Assert
            await Should.ThrowAsync<TournamentActiveDateMustStartOnMondayException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
