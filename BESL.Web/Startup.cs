namespace BESL.Web
{
    using FluentValidation.AspNetCore;
    using Hangfire;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    
    using BESL.Application;
    using BESL.Application.Interfaces;
    using BESL.Infrastructure;
    using BESL.Infrastructure.Hubs;
    using BESL.Persistence;
    using BESL.Web.Filters;
    using BESL.Web.Infrastructure;
    using BESL.Web.Middlewares;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddInfrastructureLayer(this.Configuration);
            services.AddPersistenceLayer(this.Configuration);
            services.AddApplicationLayer(this.Configuration);
            services.AddSignalR();
            services.AddHttpContextAccessor();
            services.AddAuthentication().AddSteam();
            services.AddRazorPages();
            services.AddHangfire(cfg => cfg.UseSqlServerStorage(this.Configuration.GetConnectionString("Hangfire")));
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IApplicationDbContext>())
                .AddRazorRuntimeCompilation();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }
    
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            HangfireJobScheduler.ScheduleRecurringJobs();
        }
    }
}
