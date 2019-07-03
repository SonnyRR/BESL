namespace BESL.Application.Tests.Infrastructure
{
    using System;
    using BESL.Persistence;

    public class BaseCommandTest : IDisposable
    {

        protected readonly ApplicationDbContext dbContext;

        public BaseCommandTest()
        {
            this.dbContext = ApplicationDbContextFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(this.dbContext);
        }
    }
}
