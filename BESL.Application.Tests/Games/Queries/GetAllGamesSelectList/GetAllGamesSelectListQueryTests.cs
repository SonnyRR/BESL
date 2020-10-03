namespace BESL.Application.Tests.Games.Queries.GetAllGamesSelectList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using MockQueryable.Moq;
    using Xunit;
    using Shouldly;

    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Games.Queries.GetAllGamesSelectList;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Entities;

    public class GetAllGamesSelectListQueryTests
    {
        private readonly IMapper mapper;

        public GetAllGamesSelectListQueryTests()
        {
            this.mapper = AutoMapperFactory.Create();
        }

        [Trait(nameof(Game), "GetAllGamesSelectList query tests.")]
        [Fact(DisplayName = "Handle given valid request should return valid GamesSelectList viewmodel.")]
        public async Task Handle_GivenValidRequest_ShouldReturnValidViewModel()
        {
            // Arrange           
            var query = new GetAllGamesSelectListQuery();
            var gameRepositoryMock = new Mock<IDeletableEntityRepository<Game>>();
            var dataSet = new List<Game>()
            {
                new Game()
                {
                    Id = 1,
                    Name = It.IsAny<string>(),
                    Description = It.IsAny<string>(),
                    CreatedOn = It.IsAny<DateTime>(),
                    TournamentFormats = It.IsAny<ICollection<TournamentFormat>>(),
                    GameImageUrl = It.IsAny<string>()
                },
                new Game()
                {
                    Id = 2,
                    Name = It.IsAny<string>(),
                    Description = It.IsAny<string>(),
                    CreatedOn = It.IsAny<DateTime>(),
                    TournamentFormats = It.IsAny<ICollection<TournamentFormat>>(),
                    GameImageUrl = It.IsAny<string>()
                },
                new Game()
                {
                    Id = 3,
                    Name = "CS:GO",
                    Description = It.IsAny<string>(),
                    CreatedOn = It.IsAny<DateTime>(),
                    TournamentFormats = It.IsAny<ICollection<TournamentFormat>>(),
                    GameImageUrl = It.IsAny<string>()
                },
            }.AsQueryable();

            var dataSetMock = dataSet.BuildMock();
            gameRepositoryMock.Setup(m => m.AllAsNoTracking()).Returns(dataSetMock.Object);

            var sut = new GetAllGamesSelectListQueryHandler(gameRepositoryMock.Object, this.mapper);

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.ShouldBeAssignableTo<IEnumerable<GameSelectItemLookupModel>>();
            result.Count().ShouldBe(3);
            result.SingleOrDefault(g => g.Id == 3).Name.ShouldBe("CS:GO");
        }

        [Trait(nameof(Game), "GetAllGamesSelectList query tests.")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException.")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Act
            var sut = new GetAllGamesSelectListQueryHandler(It.IsAny<IDeletableEntityRepository<Game>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
