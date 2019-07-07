namespace BESL.Application.Tests.Infrastructure
{
    using System;
    using AutoMapper;
    using BESL.Persistence;
    using Xunit;

    public class QueryTestFixture : IDisposable
    {
        public ApplicationDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = ApplicationDbContextFactory.Create();
            Mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture>
    {
    }
}
