namespace BESL.Application.Tests.Teams.Queries.GetTeamTournamentsMatches
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.Teams.Queries.GetTeamTournamentsMatches;
    using BESL.Application.Tests.Infrastructure;
    using Match = Domain.Entities.Match;

    public class GetTeamTournamentsMatchesQueryTests : BaseTest<Match>
    {
        [Trait(nameof(Team), "GetTeamTournamentsMatches query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetTeamTournamentsMatchesQuery { TeamId = 1 };
            var sut = new GetTeamTournamentsMatchesQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<GetTeamTournamentsMatchesViewModel>();
            viewModel.TournamentMatches.ShouldNotBeEmpty();
        }

        [Trait(nameof(Team), "GetTeamTournamentsMatches query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetTeamTournamentsMatchesQueryHandler(It.IsAny<IDeletableEntityRepository<Match>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
