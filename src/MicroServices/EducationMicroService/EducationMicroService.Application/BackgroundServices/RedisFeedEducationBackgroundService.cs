using Core.CacheService;
using EducationMicroService.Application.Contracts;
using EducationMicroService.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EducationMicroService.Application.BackgroundServices
{
    public class RedisFeedEducationBackgroundService : BackgroundService
    {
        private readonly ILogger<RedisFeedEducationBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public RedisFeedEducationBackgroundService(ILogger<RedisFeedEducationBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IEducationRepository _educationRepository = scope.ServiceProvider.GetRequiredService<IEducationRepository>();
                IDistributedCacheService<Education> _distributedCacheService = scope.ServiceProvider.GetRequiredService<IDistributedCacheService<Education>>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var educationList = await _educationRepository.GetAllAsync();
                        foreach (var item in educationList)
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
