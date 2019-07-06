namespace BESL.Application.Tests.Infrastructure
{
    using System;
    using BESL.Persistence;

    public class BaseTest : IDisposable
    {

        protected readonly ApplicationDbContext dbContext;

        public BaseTest()
        {
            this.dbContext = ApplicationDbContextFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(this.dbContext);
        }
    }
}
