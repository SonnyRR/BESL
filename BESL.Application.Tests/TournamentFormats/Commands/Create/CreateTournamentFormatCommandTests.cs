namespace BESL.Application.Tests.TournamentFormats.Commands.Create
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using MockQueryable.Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.TournamentFormats.Commands.Create;
    using BESL.Entities;
    using BESL.Persistence.Repositories;

    public class CreateTournamentFormatCommandTests : BaseTest<TournamentFormat>
    {
        [Trait(nameof(TournamentFormat), "CreateTournamentFormat command tests.")]
        [Fact(DisplayName = "Handle given valid request should create valid entity.")]
        public async Task Handle_GivenValidRequest_ShouldCreateValidEntity()
        {
            // Arrange
            var request = new CreateTournamentFormatCommand()
            {
                GameId = 2,
                Name = "5v5",
                Description = "Plain old 5v5 classic counter-strike competitive format",
                TeamPlayersCount = 5
            };

            IDeletableEntityRepository<Game> gameRepo = new EfDeletableEntityRepository<Game>(this.dbContext);
            var sut = new CreateTournamentFormatCommandHandler(base.deletableEntityRepository, gameRepo);

            // Act
            var result = await sut.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBe(4);
        }

        [Trait(nameof(TournamentFormat), "CreateTournamentFormat command tests.")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException.")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new CreateTournamentFormatCommand()
            {
                GameId = 90125,
                Name = "5v5",
                Description = "Plain old 5v5 classic counter-strike competitie format",
                TeamPlayersCount = 5
            };

            IDeletableEntityRepository<Game> gameRepo = new EfDeletableEntityRepository<Game>(this.dbContext);
            var sut = new CreateTournamentFormatCommandHandler(deletableEntityRepository, gameRepo);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(request, CancellationToken.None));
        }

        [Trait(nameof(TournamentFormat), "CreateTournamentFormat command tests.")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException.")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var tournamentFormatsRepositoryMock = new Mock<IDeletableEntityRepository<TournamentFormat>>();

            var dataSet = new List<TournamentFormat>
            {
                new TournamentFormat
                {
                     Name = "5v5",
                     GameId = 123,
                     Id = It.IsAny<int>(),
                     TeamPlayersCount = 5,
                     TotalPlayersCount = 10,
                     Description = It.IsAny<string>()
                }

            }.AsQueryable();

            var dataSetMock = dataSet.BuildMock();
            tournamentFormatsRepositoryMock.Setup(m => m.AllAsNoTrackingWithDeleted()).Returns(dataSetMock.Object);

            var request = new CreateTournamentFormatCommand { Name = "5v5", GameId = 123 };
            var sut = new CreateTournamentFormatCommandHandler(tournamentFormatsRepositoryMock.Object, It.IsAny<IDeletableEntityRepository<Game>>());

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(request, CancellationToken.None));
        }
    }
}
