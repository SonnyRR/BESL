namespace BESL.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using BESL.Persistence.Infrastructure;
    using BESL.Infrastructure;

    public class ApplicationContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
    {
        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options, new MachineDateTime());
        }
    }
}
