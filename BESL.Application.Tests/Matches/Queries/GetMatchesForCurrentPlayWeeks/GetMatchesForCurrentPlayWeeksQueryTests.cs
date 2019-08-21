namespace BESL.Application.Tests.Matches.Queries.GetMatchesForCurrentPlayWeeks
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Matches.Queries.GetMatchesForCurrentPlayWeeks;
    using BESL.Domain.Entities;
    using BESL.Persistence.Repositories;
    using Match = Domain.Entities.Match;

    public class GetMatchesForCurrentPlayWeeksQueryTests : BaseTest<Match>
    {
        [Trait(nameof(Match), "GetMatchesForCurrentPlayWeeks query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetMatchesForCurrentPlayWeeksQuery();
            var sut = new GetMatchesForCurrentPlayWeeksQueryHandler(this.deletableEntityRepository, this.mapper);

            this.dbContext.Matches.AddRange(new[]
            {
                new Match{ HomeTeamId = 1, AwayTeamId = 2, PlayWeekId = 3 },
                new Match{ HomeTeamId = 1, AwayTeamId = 2, PlayWeekId = 3 },
            });
            this.dbContext.SaveChanges();

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.Matches.Count().ShouldBe(2);
        }

        [Trait(nameof(Match), "GetMatchesForCurrentPlayWeeks query tests")]
        [Fact(DisplayName = "Handle given valid null should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetMatchesForCurrentPlayWeeksQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
