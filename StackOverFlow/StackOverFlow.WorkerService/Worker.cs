using Autofac;
using Microsoft.AspNetCore.Identity;
using StackOverFlow.WorkerService.Models;

namespace StackOverFlow.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly ILifetimeScope _scope;


        public Worker(ILogger<Worker> logger,ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(300000, stoppingToken);
            }
        }
    }
}