namespace BESL.Application.Tests.Games.Queries.ModifyGame
{
    using AutoMapper;
    using BESL.Application.Exceptions;
    using BESL.Application.Games.Queries.ModifyGame;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
    using BESL.Persistence;
    using Shouldly;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("QueryCollection")]
    public class ModifyGameQueryTests
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ModifyGameQueryTests(QueryTestFixture fixture)
        {
            this.mapper = fixture.Mapper;
            this.dbContext = fixture.Context;
        }

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "ModifyGameQuery handler given valid request should return valid GameDetails viewmodel.")]
        public async Task Handle_GivenValidRequest_ShouldReturnValidViewModel()
        {
            // Arrange
            var request = new ModifyGameQuery() { Id = 2 };
            var sut = new ModifyGameQueryHandler(this.dbContext);

            // Act
            var result = await sut.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<ModifyGameViewModel>();
            result.Id.ShouldBe(2);
        }

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "ModifyGameQuery handler given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new ModifyGameQuery() { Id = 90125 };
            var sut = new ModifyGameQueryHandler(this.dbContext);

            // Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(request, CancellationToken.None));
        }

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "ModifyGameQuery handler given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new ModifyGameQueryHandler(this.dbContext);

            // Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, CancellationToken.None));
        }
    }
}
