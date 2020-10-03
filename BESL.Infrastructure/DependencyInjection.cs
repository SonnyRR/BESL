namespace BESL.Infrastructure
{
    using System;

    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using BESL.Application.Interfaces;
    using BESL.Infrastructure.Cloudinary;
    using BESL.Infrastructure.Hubs;
    using BESL.Infrastructure.Messaging;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddScoped<INotifyService, UserNotificationHub>();
            services.AddTransient<IEmailSender, BeslMessageSender>();
            services.AddTransient<ISmsSender, BeslMessageSender>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddSingleton(x => CloudinaryFactory.GetInstance(configuration));
            services.AddTransient<ICloudinaryHelper, CloudinaryHelper>();

            return services;
        }
    }
}
