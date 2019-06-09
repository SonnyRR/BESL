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
            DbContextOptions<ApplicationDbContext> options = new DbContextOptions<ApplicationDbContext>();
            factory.CreateDbContext(null);

        }
    }
}
