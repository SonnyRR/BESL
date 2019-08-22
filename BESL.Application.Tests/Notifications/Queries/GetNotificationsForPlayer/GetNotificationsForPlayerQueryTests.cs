namespace BESL.Application.Tests.Notifications.Queries.GetNotificationsForPlayer
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
    using BESL.Application.Notifications.Queries.GetNotificationsForPlayer;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;

    public class GetNotificationsForPlayerQueryTests : BaseTest<Notification>
    {
        [Trait(nameof(Notification), "GetNotificationsForPlayer query tests")]
        [Fact(DisplayName ="Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetNotificationsForPlayerQuery { UserId = "Foo1" };
            var sut = new GetNotificationsForPlayerQueryHandler(this.deletableEntityRepository, this.mapper);

            this.dbContext.Notifications.Add(new Notification { PlayerId = "Foo1", Content = "TestContent", Header = "TestHeader", Type = Domain.Entities.Enums.NotificationType.Info });
            this.dbContext.SaveChanges();

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.Notifications.Count().ShouldBe(1);            
        }

        [Trait(nameof(Notification), "GetNotificationsForPlayer query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetNotificationsForPlayerQueryHandler(It.IsAny<IDeletableEntityRepository<Notification>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
