namespace BESL.Application.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IDbSeeder
    {
        Task SeedAsync(IApplicationDbContext databaseContext, IServiceProvider serviceProvider);
    }
}
