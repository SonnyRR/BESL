namespace BESL.Application.Tests.Teams.Commands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Commands.Create;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Entities;
    using BESL.Persistence.Repositories;

    public class CreateTeamCommandTests : BaseTest<Team>
    {
        [Trait(nameof(Team), "CreateTeam command tests")]
        [Fact(DisplayName = "Handle given valid request should create team")]
        public async Task Handle_GivenValidRequest_ShouldCreateTeam()
        {
            // Arrange
            var command = new CreateTeamCommand
            {
                Name = "ValidTeam",
                Description = "SomeDescriptionIDontKnow",
                TournamentFormatId = 2,
                TeamImage = new Mock<IFormFile>().Object
            };

            var cloudinaryMock = new Mock<ICloudinaryHelper>();
            cloudinaryMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                          .ReturnsAsync("http://abcdef.g/123.jpg");

            var tournamentFormatsRepository = new EfDeletableEntityRepository<TournamentFormat>(this.dbContext);
            var playerTeamsRepository = new EfDeletableEntityRepository<PlayerTeam>(this.dbContext);
            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new CreateTeamCommandHandler(this.deletableEntityRepository, tournamentFormatsRepository, playerTeamsRepository, playersRepository, cloudinaryMock.Object, this.mapper, userAccessorMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldBeGreaterThan(0);

            var createdTeam = this.deletableEntityRepository.AllAsNoTracking().SingleOrDefault(x => x.Name == "ValidTeam");

            createdTeam.Description.ShouldBe("SomeDescriptionIDontKnow");
            createdTeam.ImageUrl.ShouldBe("http://abcdef.g/123.jpg");
            createdTeam.TournamentFormatId.ShouldBe(2);
        }

        [Trait(nameof(Team), "CreateTeam command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateTeamCommandHandler(
                It.IsAny<IDeletableEntityRepository<Team>>(),
                It.IsAny<IDeletableEntityRepository<TournamentFormat>>(),
                It.IsAny<IDeletableEntityRepository<PlayerTeam>>(),
                It.IsAny<IDeletableEntityRepository<Player>>(),
                It.IsAny<ICloudinaryHelper>(),
                It.IsAny<IMapper>(),
                It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "CreateTeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw PlayerDoesNotHaveALinkedSteamAccountException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowPlayerDoesNotHaveALinkedSteamAccountException()
        {
            // Arrange
            var command = new CreateTeamCommand
            {
                Name = "ValidTeam",
                Description = "SomeDescriptionIDontKnow",
                TournamentFormatId = 2,
                TeamImage = It.IsAny<IFormFile>()
            };

            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo2");

            var sut = new CreateTeamCommandHandler(
                It.IsAny<IDeletableEntityRepository<Team>>(),
                It.IsAny<IDeletableEntityRepository<TournamentFormat>>(),
                It.IsAny<IDeletableEntityRepository<PlayerTeam>>(),
                playersRepository,
                It.IsAny<ICloudinaryHelper>(),
                It.IsAny<IMapper>(),
                userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<PlayerDoesNotHaveALinkedSteamAccountException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "CreateTeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new CreateTeamCommand
            {
                Name = "ValidTeam",
                Description = "SomeDescriptionIDontKnow",
                TournamentFormatId = 223,
                TeamImage = It.IsAny<IFormFile>()
            };

            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);
            var tournamentFormatsRepository = new EfDeletableEntityRepository<TournamentFormat>(this.dbContext);

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new CreateTeamCommandHandler(
                It.IsAny<IDeletableEntityRepository<Team>>(),
                tournamentFormatsRepository,
                It.IsAny<IDeletableEntityRepository<PlayerTeam>>(),
                playersRepository,
                It.IsAny<ICloudinaryHelper>(),
                It.IsAny<IMapper>(),
                userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "CreateTeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowPlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException()
        {
            // Arrange
            var command = new CreateTeamCommand
            {
                Name = "ValidTeam",
                Description = "SomeDescriptionIDontKnow",
                TournamentFormatId = 1,
                TeamImage = It.IsAny<IFormFile>()
            };

            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);
            var tournamentFormatsRepository = new EfDeletableEntityRepository<TournamentFormat>(this.dbContext);
            var playerTeamsRepository = new EfDeletableEntityRepository<PlayerTeam>(this.dbContext);

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new CreateTeamCommandHandler(
                It.IsAny<IDeletableEntityRepository<Team>>(),
                tournamentFormatsRepository,
                playerTeamsRepository,
                playersRepository,
                It.IsAny<ICloudinaryHelper>(),
                It.IsAny<IMapper>(),
                userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "CreateTeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new CreateTeamCommand
            {
                Name = "FooTeam2",
                Description = "SomeDescriptionIDontKnow",
                TournamentFormatId = 2,
                TeamImage = It.IsAny<IFormFile>()
            };

            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);
            var tournamentFormatsRepository = new EfDeletableEntityRepository<TournamentFormat>(this.dbContext);
            var playerTeamsRepository = new EfDeletableEntityRepository<PlayerTeam>(this.dbContext);

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new CreateTeamCommandHandler(
                this.deletableEntityRepository,
                tournamentFormatsRepository,
                playerTeamsRepository,
                playersRepository,
                It.IsAny<ICloudinaryHelper>(),
                It.IsAny<IMapper>(),
                userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
