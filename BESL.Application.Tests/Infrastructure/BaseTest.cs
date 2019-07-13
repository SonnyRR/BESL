namespace BESL.Application.Tests.Infrastructure
{
    using System;
    using AutoMapper;
    using BESL.Application.Interfaces;
    using BESL.Domain.Infrastructure;
    using BESL.Persistence;
    using BESL.Persistence.Repositories;

    public class BaseTest<T> : IDisposable
        where T : class, IDeletableEntity, new()
    { 
        protected readonly ApplicationDbContext dbContext;
        protected readonly IMapper mapper;
        protected readonly IRepository<T> repository;
        protected readonly IDeletableEntityRepository<T> deletableEntityRepository;

        public BaseTest()
        {
            this.dbContext = ApplicationDbContextFactory.Create().GetAwaiter().GetResult();
            this.mapper = AutoMapperFactory.Create();
            this.repository = new EfRepository<T>(this.dbContext);
            this.deletableEntityRepository = new EfDeletableEntityRepository<T>(this.dbContext);

        }

        public void Dispose()
        {
            this.repository.Dispose();
            this.deletableEntityRepository.Dispose();
            //ApplicationDbContextFactory.Destroy(this.dbContext);
        }
    }
}
