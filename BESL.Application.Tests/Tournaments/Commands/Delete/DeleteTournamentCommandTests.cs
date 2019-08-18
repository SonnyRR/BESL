namespace BESL.Application.Tests.Tournaments.Commands.Delete
{
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Tournaments.Commands.Delete;
    using BESL.Domain.Entities;
    using Moq;
    using Shouldly;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class DeleteTournamentCommandTests : BaseTest<Tournament>
    {
        [Trait(nameof(Tournament), "DeleteTournament command tests")]
        [Fact(DisplayName = "Handle given valid request should delete entity")]

        public async Task Handle_GivenValidRequest_ShouldDeleteEntity()
        {
            // Arrange
            var command = new DeleteTournamentCommand { Id = 2 };
            var sut = new DeleteTournamentCommandHandler(this.deletableEntityRepository);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldBeGreaterThan(0);

            var deletedTournament = this.deletableEntityRepository.AllAsNoTrackingWithDeleted().SingleOrDefault(x => x.Id == 2);
            deletedTournament.IsDeleted.ShouldBe(true);
        }
    }
}
