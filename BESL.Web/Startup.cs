namespace BESL.Web
{
    using System.Linq;
    using System.Reflection;

    using AutoMapper;
    using FluentValidation.AspNetCore;
    using Hangfire;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using BESL.Application;
    using BESL.Application.Infrastructure;
    using BESL.Application.Interfaces;
    using BESL.Application.Infrastructure.AutoMapper;
    using BESL.Application.Infrastructure.Validators;
    using BESL.Common;
    using BESL.Domain.Entities;
    using BESL.Infrastructure;
    using BESL.Infrastructure.Messaging;
    using BESL.Infrastructure.Cloudinary;
    using BESL.Infrastructure.Hubs;
    using BESL.Infrastructure.CronJobs;
    using BESL.Persistence;
    using BESL.Persistence.Seeding;
    using BESL.Persistence.Repositories;   
    using BESL.Persistence.Infrastructure;
    using BESL.Web.Filters;
    using BESL.Web.Infrastructure;
    using BESL.Web.Middlewares;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        this.Configuration
                            .GetConnectionString(DbConnectionStringHandler.GetConnectionStringNameForCurrentOS())));

            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        this.Configuration
                            .GetConnectionString(DbConnectionStringHandler.GetConnectionStringNameForCurrentOS())));

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

            services.AddAuthentication().AddSteam();

            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).Assembly });

            services.AddRazorPages();
            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ApplicationDependencyInjectionHelper>());

            services.AddMediatR(typeof(ApplicationDependencyInjectionHelper).Assembly);

            services.AddTransient<IDateTime, MachineDateTime>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CustomExceptionNotificationBehaviour<,>));
            
            services.AddScoped<IFileValidate, GameImageFileValidate>();
            services.AddScoped<INotifyService, UserNotificationHub>();

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddSignalR();
            services.AddHttpContextAccessor();

            services.AddHangfire(cfg => cfg.UseSqlServerStorage(
                this.Configuration.GetConnectionString(DbConnectionStringHandler.GetHangfireConnectionStringNameForCurrentOS())));

            services.AddTransient<CheckForVACBans>();

            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISmsSender, NullMessageSender>();

            services.AddScoped<IUserAccessor, UserAccessor>();

            services.AddSingleton(x => CloudinaryFactory.GetInstance(this.Configuration));
            services.AddTransient<ICloudinaryHelper, CloudinaryHelper>();

            services.Configure<RedisConfigurationOptions>(Configuration.GetSection("Redis"));
            services.AddTransient(typeof(IRedisService<>), typeof(RedisService<>));
        }
    
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                //if (env.IsDevelopment())
                //{
                //    #warning Note that this API is mutually exclusive with DbContext.Database.EnsureCreated(). EnsureCreated does not use migrations to create the database and therefore the database that is created cannot be later updated using migrations.
                //    this.ApplyMigrations(dbContext);
                //}

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");            
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireServer();
            app.UseHangfireDashboard("/Administration/hangfire", new DashboardOptions()
            {
                DisplayStorageConnectionString = true,
                Authorization = new[] { new DashboardAuthorizationFilter() }
            });

            HangfireJobScheduler.ScheduleRecurringJobs();

            app.UseNotificationHandlerMiddleware();
            app.UseCustomExceptionHandlerMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<UserNotificationHub>("/userNotificationHub");
                endpoints.MapRazorPages();
                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Administration",
                    pattern: "Administration/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ApplyMigrations(ApplicationDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
