namespace BESL.Application.Tests.Tournaments.Queries.GetAllTournamentsSelectList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Tournaments.Queries.GetAllTournamentsSelectList;
    using BESL.Domain.Entities;

    public class GetAllTournamentsSelectListQueryTests : BaseTest<Tournament>
    {
        [Trait(nameof(Tournament), "GetAllTournamentsSelectList query tests")]
        [Fact(DisplayName = "Handle given valid request should return IEnumerable")]
        public async Task Handle_GivenValidRequest_ShouldReturnIEnumerable()
        {
            // Arrange
            var query = new GetAllTournamentsSelectListQuery();
            var sut = new GetAllTournamentsSelectListQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var collection = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            collection.ShouldNotBeNull();
            collection.Count().ShouldBeGreaterThan(0);
        }

        [Trait(nameof(Tournament), "GetAllTournamentsSelectList query tests")]
        [Fact(DisplayName = "Handle given valid result should return IEnumerable")]
        public async Task Handle_GivenValidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllTournamentsSelectListQueryHandler(It.IsAny<IDeletableEntityRepository<Tournament>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
