namespace BESL.Application.Tests.Tournaments.Commands.Enroll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MockQueryable.Moq;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Tournaments.Commands.Enroll;
    using BESL.Entities;
    using BESL.Persistence.Repositories;

    public class EnrollATeamCommandTests : BaseTest<Tournament>
    {
        [Trait(nameof(Tournament), "EnrollATeam command tests")]
        [Fact(DisplayName = "Handle given valid request should enroll team")]
        public async Task Handle_GivenValidRequest_ShouldEnrollTeam()
        {
            // Arrange
            var command = new EnrollATeamCommand { TableId = 2, TeamId = 2 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo2");


            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);
            var teamsRepository = new EfDeletableEntityRepository<Team>(this.dbContext);
            var tournamentTablesRepository = new EfDeletableEntityRepository<TournamentTable>(this.dbContext);

            var sut = new EnrollATeamCommandHandler(
                teamsRepository,
                playersRepository,
                tournamentTablesRepository,
                userAccessorMock.Object);

            // Act
            var teamToBeEnrolled = teamsRepository.All().FirstOrDefault(t => t.Id == 2);
            for (int i = 0; i < 6; i++)
            {
                teamToBeEnrolled.PlayerTeams.Add(new PlayerTeam() { PlayerId = $"FooPlayer{i + 11}", TeamId = 2 });
            }

            await teamsRepository.SaveChangesAsync();

            var rowsAffected = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            rowsAffected.ShouldBe(2);
        }


        [Trait(nameof(Tournament), "EnrollATeam command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new EnrollATeamCommandHandler(
                It.IsAny<IDeletableEntityRepository<Team>>(),
                It.IsAny<IDeletableEntityRepository<Player>>(),
                It.IsAny<IDeletableEntityRepository<TournamentTable>>(),
                It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "EnrollATeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException(Player)")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundExceptionForPlayer()
        {
            // Arrange
            var command = new EnrollATeamCommand();

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("randomInvalidId");

            var dataSet = new List<Player>(0).AsQueryable();
            var dataSetMock = dataSet.BuildMock();

            var playersRepositoryMock = new Mock<IDeletableEntityRepository<Player>>();
            playersRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(dataSetMock.Object);

            var sut = new EnrollATeamCommandHandler(
                It.IsAny<IDeletableEntityRepository<Team>>(),
                playersRepositoryMock.Object,
                It.IsAny<IDeletableEntityRepository<TournamentTable>>(),
                userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "EnrollATeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException(TournamentTable)")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundExceptionForTournamentTable()
        {
            // Arrange
            var command = new EnrollATeamCommand { TableId = 12345 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo2");

            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);

            var dataSet = new List<TournamentTable>(0).AsQueryable();
            var dataSetMock = dataSet.BuildMock();
            var tournamentTablesRepositoryMock = new Mock<IDeletableEntityRepository<TournamentTable>>();
            tournamentTablesRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(dataSetMock.Object);

            var sut = new EnrollATeamCommandHandler(
                It.IsAny<IDeletableEntityRepository<Team>>(),
                playersRepository,
                tournamentTablesRepositoryMock.Object,
                userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "EnrollATeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw PlayerHasAlreadyEnrolledTeamException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowPlayerHasAlreadyEnrolledTeamException()
        {
            // Arrange
            var command = new EnrollATeamCommand { TableId = 2, TeamId = 2 };
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);
            var teamsRepository = new EfDeletableEntityRepository<Team>(this.dbContext);
            var tournamentTablesRepository = new EfDeletableEntityRepository<TournamentTable>(this.dbContext);

            var sut = new EnrollATeamCommandHandler(
                teamsRepository,
                playersRepository,
                tournamentTablesRepository,
                userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<PlayerHasAlreadyEnrolledTeamException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "EnrollATeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw TournamentTableIsFullException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowTournamentTableIsFullException()
        {
            // Arrange
            var command = new EnrollATeamCommand { TableId = 2, TeamId = 2 };
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var dataSet = new List<TournamentTable>(1) {
                new TournamentTable
                {
                    Id = 2,
                    MaxNumberOfTeams = 1,
                    TeamTableResults = new HashSet<TeamTableResult>(1)
                    {
                        new TeamTableResult()
                    }
                }
            }.AsQueryable();

            var dataSetMock = dataSet.BuildMock();
            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);
            var teamsRepository = new EfDeletableEntityRepository<Team>(this.dbContext);
            var tournamentTablesRepositoryMock = new Mock<IDeletableEntityRepository<TournamentTable>>();
            tournamentTablesRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(dataSetMock.Object);

            var sut = new EnrollATeamCommandHandler(
                teamsRepository,
                playersRepository,
                tournamentTablesRepositoryMock.Object,
                userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<TournamentTableIsFullException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "EnrollATeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException(Team)")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundExceptionForTeam()
        {
            // Arrange
            var command = new EnrollATeamCommand { TableId = 2, TeamId = 55 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo3");

            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);
            var teamsRepository = new EfDeletableEntityRepository<Team>(this.dbContext);
            var tournamentTablesRepository = new EfDeletableEntityRepository<TournamentTable>(this.dbContext);

            var sut = new EnrollATeamCommandHandler(
                teamsRepository,
                playersRepository,
                tournamentTablesRepository,
                userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "EnrollATeam command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw TeamFormatDoesNotMatchTournamentFormatException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowTeamFormatDoesNotMatchTournamentFormatException()
        {
            // Arrange
            var command = new EnrollATeamCommand { TableId = 2, TeamId = 123 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo2");

            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);
            var tournamentTablesRepository = new EfDeletableEntityRepository<TournamentTable>(this.dbContext);
             
            var dataSet = new List<Team>(1)
            {
                new Team { Id = 123, OwnerId = "Foo2", TournamentFormatId = 6969, Name = "MockTeam" }
            }
            .AsQueryable()
            .BuildMock();

            var teamsMockRepository = new Mock<IDeletableEntityRepository<Team>>();
            teamsMockRepository.Setup(x => x.AllAsNoTracking()).Returns(dataSet.Object);

            var sut = new EnrollATeamCommandHandler(
                teamsMockRepository.Object,
                playersRepository,
                tournamentTablesRepository,
                userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<TeamFormatDoesNotMatchTournamentFormatException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
