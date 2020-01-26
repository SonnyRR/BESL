namespace BESL.Application.Tests.Infrastructure
{
    using System;

    using AutoMapper;
    using Xunit;
    
    using BESL.Persistence;

    public class QueryTestFixture : IDisposable
    {
        public ApplicationDbContext Context { get; private set; }

        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            this.Context = ApplicationDbContextFactory.Create();
            this.Mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(this.Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture>
    {
    }
}
