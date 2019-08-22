namespace BESL.Application.Tests.Notifications.Commands.Delete
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
    using BESL.Application.Notifications.Commands.Delete;
    using BESL.Application.Exceptions;

    public class DeleteNotificationCommandTests : BaseTest<Notification>
    {
        [Trait(nameof(Notification), "DeleteNotification command tests")]
        [Fact(DisplayName = "Handle given valid request should delete notification")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var notification = new Notification { PlayerId = "Foo1", Content = "TestContent", Header = "TestHeader", Type = Domain.Entities.Enums.NotificationType.Info };
            this.dbContext.Add(notification);
            this.dbContext.SaveChanges();
            this.dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.State = Microsoft.EntityFrameworkCore.EntityState.Detached);

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var command = new DeleteNotificationCommand { Id = notification.Id };
            var sut = new DeleteNotificationCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldNotBeEmpty();

            var notificationDeleted = dbContext.Notifications.SingleOrDefault();
            notificationDeleted.IsDeleted.ShouldBe(true);
        }

        [Trait(nameof(Notification), "DeleteNotification command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteNotificationCommandHandler(It.IsAny<IDeletableEntityRepository<Notification>>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Notification), "DeleteNotification command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var command = new DeleteNotificationCommand { Id = "InvalidId" };
            var sut = new DeleteNotificationCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Notification), "DeleteNotification command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw DeleteFailureException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowDeleteFailureException()
        {
            // Arrange
            var notification = new Notification { PlayerId = "Foo1", Content = "TestContent", Header = "TestHeader", Type = Domain.Entities.Enums.NotificationType.Info };
            this.dbContext.Add(notification);
            this.dbContext.SaveChanges();

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo2");

            var command = new DeleteNotificationCommand { Id = notification.Id };
            var sut = new DeleteNotificationCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<DeleteFailureException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
