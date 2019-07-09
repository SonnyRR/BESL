namespace BESL.Application.Tests.Infrastructure
{
    using System;
    using AutoMapper;
    using BESL.Persistence;

    public class BaseTest : IDisposable
    { 
        protected readonly ApplicationDbContext dbContext;
        protected readonly IMapper mapper;

        public BaseTest()
        {
            this.dbContext = ApplicationDbContextFactory.Create().GetAwaiter().GetResult();
            this.mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(this.dbContext);
        }
    }
}
