using Example.MassTransit.Contracts;

using MassTransit;

using Serilog;

namespace Example.MassTransit.Consumer;

public class SubmitOrderConsumer(ILogger logger) : IConsumer<SubmitOrder>
{
    private readonly ILogger _logger = logger;

    public Task Consume(ConsumeContext<SubmitOrder> context)
    {
        _logger.Information("Order Received: {OrderId}", context.Message.OrderId);
        return Task.CompletedTask;
    }
}
