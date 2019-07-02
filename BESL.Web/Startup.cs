namespace BESL.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using MediatR;

    using BESL.Application;
    using BESL.Application.Games.Commands.Create;
    using BESL.Application.Interfaces;
    using BESL.Application.Infrastructure.AutoMapper;
    using BESL.Application.Infrastructure.Validators;
    using BESL.Common;
    using BESL.Domain.Entities;
    using BESL.Persistence;
    using BESL.Web.Middlewares;
    using BESL.Web.Hubs;
    using BESL.Web.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
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

            services.AddIdentity<ApplicationUser, IdentityRole>(
                opt =>
                {
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequireUppercase = true;
                    // opt.User.RequireUniqueEmail = true;
                })
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication().AddSteam();

            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).Assembly });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ApplicationDependencyInjectionHelper>());

            services.AddMediatR(typeof(ApplicationDependencyInjectionHelper).Assembly);

            services.AddScoped<IFileValidate, GameImageFileValidate>();
            services.AddScoped<INotifyService, NotifyService>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSeedMiddleware();

            app.UseSignalR(routeBuilder =>
            {
                routeBuilder.MapHub<UserNotificationHub>("/userNotificationHub");
            });

            app.UseCustomExceptionHandlerMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}