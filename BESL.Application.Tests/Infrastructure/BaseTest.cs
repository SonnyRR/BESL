namespace BESL.Application.Tests.Infrastructure
{
    using System;
    using AutoMapper;

    using MediatR;
    using Moq;

    using BESL.Application.Interfaces;
    using BESL.Entities.Infrastructure;
    using BESL.Persistence;
    using BESL.Persistence.Repositories;

    public abstract class BaseTest<T> : IDisposable
        where T : class, IDeletableEntity, new()
    { 
        protected readonly ApplicationDbContext dbContext;
        protected readonly IMapper mapper;
        protected readonly Mock<IMediator> mediatorMock;
        protected readonly IDeletableEntityRepository<T> deletableEntityRepository;

        public BaseTest()
        {
            this.dbContext = ApplicationDbContextFactory.Create();
            this.mapper = AutoMapperFactory.Create();
            this.deletableEntityRepository = new EfDeletableEntityRepository<T>(this.dbContext);
            this.mediatorMock = new Mock<IMediator>();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(dbContext);
            this.deletableEntityRepository.Dispose();
        }
    }
}
