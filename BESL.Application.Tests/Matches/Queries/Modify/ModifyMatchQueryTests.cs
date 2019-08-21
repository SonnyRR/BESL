namespace BESL.Application.Tests.Matches.Queries.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Matches.Commands.Modify;
    using BESL.Application.Matches.Queries.Modify;
    using BESL.Domain.Entities;
    using Match = Domain.Entities.Match;

    public class ModifyMatchQueryTests : BaseTest<Match>
    {
        [Trait(nameof(Match), "ModifyMatch query tests")]
        [Fact(DisplayName = "Handle given valid request should return command")]
        public async Task Handle_GivenValidRequest_ShouldReturnCommand()
        {
            // Arrange
            var query = new ModifyMatchQuery { MatchId = 2 };
            var sut = new ModifyMatchQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            command.ShouldNotBeNull();
            command.ShouldBeOfType<ModifyMatchCommand>();
            command.Id.ShouldBe(2);
            command.HomeTeamId.ShouldBe(1);
            command.AwayTeamId.ShouldBe(2);
        }

        [Trait(nameof(Match), "ModifyMatch query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new ModifyMatchQueryHandler(It.IsAny<IDeletableEntityRepository<Match>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "ModifyMatch query tests")]
        [Fact(DisplayName = "Handle given null request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new ModifyMatchQuery { MatchId = 12 };
            var sut = new ModifyMatchQueryHandler(this.deletableEntityRepository, It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
