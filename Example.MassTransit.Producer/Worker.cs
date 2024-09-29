using Example.MassTransit.Contracts;

using MassTransit;

using Microsoft.Extensions.Hosting;

using Serilog;

namespace Example.MassTransit.Producer;

public class Worker(IBus bus, ILogger logger) : BackgroundService
{
    private readonly IBus _bus = bus;
    private readonly ILogger _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = new SubmitOrder(
                OrderId: Guid.NewGuid(),
                Message: $"Hello RabbitMQ! Sent at {DateTime.Now}"
            );

            await _bus.Publish(message, stoppingToken);

            _logger.Information("Order Sent: {@order}", message);

            await Task.Delay(1000, stoppingToken);
        }
    }
}
