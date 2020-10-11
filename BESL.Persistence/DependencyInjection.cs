namespace BESL.Persistence
{
    using System;
    
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using BESL.Application.Interfaces;
    using BESL.Entities;
    using BESL.Persistence.Repositories;
    using BESL.Persistence.Infrastructure;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("Database")));

            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("Database")));

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.Configure<RedisConfigurationOptions>(configuration.GetSection("Redis"));
            services.AddTransient(typeof(IRedisService<>), typeof(RedisService<>));

            services.AddTransient<IUserStore<Player>, ApplicationUserStore>();
            services.AddTransient<IRoleStore<PlayerRole>, ApplicationRoleStore>();

            services.AddIdentity<Player, PlayerRole>(
                opt =>
                {
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequireNonAlphanumeric = false;
                })
                .AddUserStore<ApplicationUserStore>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            return services;
        }
    }
}
