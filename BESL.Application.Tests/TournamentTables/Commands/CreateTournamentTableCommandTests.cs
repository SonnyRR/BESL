namespace BESL.Application.Tests.TournamentTables.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.TournamentTables.Commands.Create;
    using BESL.Entities;
    using BESL.Persistence.Repositories;

    public class CreateTournamentTableCommandTests : BaseTest<TournamentTable>
    {
        [Trait(nameof(TournamentTable), "CreateTournamentTable command tests")]
        [Fact(DisplayName = "Handle given valid request should create entity")]
        public async Task Handle_GivenValidRequest_ShouldCreateEntity()
        {
            // Arrange
            var command = new CreateTablesForTournamentCommand { TournamentId = 2 };
            var tournamentsRepository = new EfDeletableEntityRepository<Tournament>(this.dbContext);
            var sut = new CreateTablesForTournamentCommandHandler(tournamentsRepository);

            // Act
            var rowsAffected = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            rowsAffected.ShouldBe(7);
        }

        [Trait(nameof(TournamentTable), "CreateTournamentTable command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateTablesForTournamentCommandHandler(It.IsAny<IDeletableEntityRepository<Tournament>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(TournamentTable), "CreateTournamentTable command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new CreateTablesForTournamentCommand { TournamentId = 313 };
            var tournamentsRepository = new EfDeletableEntityRepository<Tournament>(this.dbContext);
            var sut = new CreateTablesForTournamentCommandHandler(tournamentsRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
