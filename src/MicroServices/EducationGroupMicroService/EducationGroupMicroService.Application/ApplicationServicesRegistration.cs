using Core.CacheService;
using EducationGroupMicroService.Application.BackgroundServices;
using EducationGroupMicroService.Application.DTOs.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EducationGroupMicroService.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateEducationGroupDtoValidator>(ServiceLifetime.Scoped);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<RedisService>(sp =>
            {
                string redisHost = configuration["RedisSettings:Host"];
                int redisPort = Convert.ToInt32(configuration["RedisSettings:Port"]);
                var redis = new RedisService(redisHost, redisPort);

                redis.Connect();

                return redis;
            });

            services.AddScoped(typeof(IDistributedCacheService<>), typeof(RedisCacheService<>));

            services.AddHostedService<RedisFeedEducationGroupBackgroundService>();

            return services;
        }
    }
}
