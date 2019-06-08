namespace BESL.Persistence
{
    using BESL.Persistence.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationContextFactory : DesignTimeDbContextFactoryBase<ApplicationContext>
    {
        protected override ApplicationContext CreateNewInstance(DbContextOptions<ApplicationContext> options)
        {
            return new ApplicationContext(options);
        }
    }
}
