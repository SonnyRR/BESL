namespace BESL.Application.Tests.Matches.Commands.Delete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Matches.Commands.Delete;
    using Match = BESL.Entities.Match;

    public class DeleteMatchCommandTests : BaseTest<Match>
    {
        [Trait(nameof(Match), "DeleteMatchCommand tests")]
        [Fact(DisplayName = "Handle given valid request should delete match")]
        public async Task Hanlde_GivenValidRequest_ShouldDeleteMatch()
        {
            // Arrange
            var command = new DeleteMatchCommand { Id = 1 };
            var sut = new DeleteMatchCommandHandler(this.deletableEntityRepository);

            // Act
            var matchId = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            matchId.ShouldBe(1);

            var matchModified = this.dbContext.Matches.SingleOrDefault(x => x.Id == matchId);
            matchModified.IsDeleted.ShouldBe(true);
        }

        [Trait(nameof(Match), "DeleteMatchCommand tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Hanlde_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteMatchCommandHandler(It.IsAny<IDeletableEntityRepository<Match>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "DeleteMatchCommand tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Hanlde_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new DeleteMatchCommand { Id = 133 };
            var sut = new DeleteMatchCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
