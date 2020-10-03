namespace BESL.Application
{
    using System;
    using System.Reflection;

    using AutoMapper;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using BESL.Application.Infrastructure;
    using BESL.Application.Infrastructure.AutoMapper;
    using BESL.Application.Infrastructure.Validators;
    using BESL.Application.Interfaces;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            services.AddMediatR(typeof(DependencyInjection).Assembly);
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).Assembly });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CustomExceptionNotificationBehaviour<,>));
            services.AddScoped<IFileValidate, GameImageFileValidate>();

            return services;
        }
    }
}
