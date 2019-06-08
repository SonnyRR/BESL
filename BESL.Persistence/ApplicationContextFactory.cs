namespace BESL.Persistence
{
    using Microsoft.EntityFrameworkCore;

    using BESL.Persistence.Infrastructure;

    public class ApplicationContextFactory : DesignTimeDbContextFactoryBase<ApplicationContext>
    {
        protected override ApplicationContext CreateNewInstance(DbContextOptions<ApplicationContext> options)
        {
            return new ApplicationContext(options);
        }
    }
}
