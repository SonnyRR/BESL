namespace BESL.Application.Tests.TournamentTables.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.TournamentTables.Commands.Create;
    using BESL.Domain.Entities;
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
    }
}
