using Core.CacheService;
using EducationGroupMicroService.Application.Contracts;
using EducationGroupMicroService.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EducationGroupMicroService.Application.BackgroundServices
{
    public class RedisFeedEducationGroupBackgroundService : BackgroundService
    {
        private readonly ILogger<RedisFeedEducationGroupBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public RedisFeedEducationGroupBackgroundService(IServiceProvider serviceProvider, ILogger<RedisFeedEducationGroupBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IEducationGroupRepository _educationGroupRepository = scope.ServiceProvider.GetRequiredService<IEducationGroupRepository>();
                IDistributedCacheService<EducationGroup> _distributedCacheService = scope.ServiceProvider.GetRequiredService<IDistributedCacheService<EducationGroup>>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var educationGroups = await _educationGroupRepository.GetAllAsync();
                        foreach (var item in educationGroups)
                        {
                            await _distributedCacheService.Set(item);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                    }
                }
            }
        }
    }
}
