namespace BESL.SandBox
{
    using BESL.Persistence;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class Program
    {
        public static void Main()
        {
            ApplicationContextFactory factory = new ApplicationContextFactory();
            DbContextOptions<ApplicationContext> options = new DbContextOptions<ApplicationContext>();
            factory.CreateDbContext(null);

        }
    }
}
