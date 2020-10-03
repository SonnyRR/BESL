namespace BESL.Application.Tests.Matches.Commands.Modify
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
    using BESL.Application.Matches.Commands.Modify;
    using Match = BESL.Entities.Match;

    public class ModifyMatchCommandTests : BaseTest<Match>
    {
        [Trait(nameof(Match), "ModifyMatchCommand tests")]
        [Fact(DisplayName = "Handle given valid request should modify match")]
        public async Task Hanlde_GivenValidRequest_ShouldModifyMatch()
        {
            // Arrange
            var command = new ModifyMatchCommand { Id = 1, HomeTeamScore = 1, AwayTeamScore = 4, ScheduledDate = new DateTime(2019, 08, 13) };
            var sut = new ModifyMatchCommandHandler(this.deletableEntityRepository);

            // Act
            var matchId = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            matchId.ShouldBe(1);

            var matchModified = this.dbContext.Matches.SingleOrDefault(x => x.Id == matchId);
            matchModified.WinnerTeamId.ShouldBe(2);
        }

        [Trait(nameof(Match), "ModifyMatchCommand tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Hanlde_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new ModifyMatchCommandHandler(It.IsAny<IDeletableEntityRepository<Match>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "ModifyMatchCommand tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Hanlde_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new ModifyMatchCommand { Id = 133 };
            var sut = new ModifyMatchCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
