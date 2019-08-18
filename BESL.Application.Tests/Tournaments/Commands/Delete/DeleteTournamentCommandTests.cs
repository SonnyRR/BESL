namespace BESL.Application.Tests.Tournaments.Commands.Delete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Tournaments.Commands.Delete;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;

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

        [Trait(nameof(Tournament), "DeleteTournament command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]

        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteTournamentCommandHandler(It.IsAny<IDeletableEntityRepository<Tournament>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "DeleteTournament command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]

        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new DeleteTournamentCommand { Id = 333 };
            var sut = new DeleteTournamentCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
