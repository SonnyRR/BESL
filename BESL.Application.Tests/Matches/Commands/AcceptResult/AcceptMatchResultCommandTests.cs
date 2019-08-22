namespace BESL.Application.Tests.Matches.Commands.AcceptResult
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Matches.Commands.AcceptResult;
    using BESL.Domain.Entities;
    using Match = Domain.Entities.Match;
    using BESL.Application.TeamTableResults.Commands.AddPoints;
    using MediatR;

    public class AcceptMatchResultCommandTests : BaseTest<Match>
    {
        [Trait(nameof(Match), "AcceptMatchResult command tests")]
        [Fact(DisplayName = "Handle given valid request should accept match result")]
        public async Task Hanlde_GivenValidRequest_ShouldAcceptMatchResult()
        {
            // Arrange
            var command = new AcceptMatchResultCommand { Id = 1 };

            var desiredMatch = this.dbContext.Matches.SingleOrDefault(x => x.Id == 1);
            desiredMatch.IsResultConfirmed = false;
            this.dbContext.SaveChanges();

            var sut = new AcceptMatchResultCommandHandler(this.deletableEntityRepository, this.mediatorMock.Object);

            // Act
            var affectedRows = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            affectedRows.ShouldBe(1);
            mediatorMock.Verify(m => m.Send(It.IsAny<AddPointsCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Trait(nameof(Match), "AcceptMatchResult command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Hanlde_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new AcceptMatchResultCommandHandler(It.IsAny<IDeletableEntityRepository<Match>>(), It.IsAny<IMediator>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "AcceptMatchResult command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Hanlde_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new AcceptMatchResultCommand { Id = 131 };
            var sut = new AcceptMatchResultCommandHandler(this.deletableEntityRepository, this.mediatorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "AcceptMatchResult command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ForbiddenException")]
        public async Task Hanlde_GivenInvalidRequest_ShouldThrowForbiddenException()
        {
            // Arrange
            var command = new AcceptMatchResultCommand { Id = 1 };
            var sut = new AcceptMatchResultCommandHandler(this.deletableEntityRepository, this.mediatorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<ForbiddenException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
