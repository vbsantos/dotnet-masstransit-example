using Example.MassTransit.Consumer;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Serilog;

using var host = Host.CreateDefaultBuilder(args)
    .UseSerilog((hostContext, loggerConfiguration) =>
    {
        var seqConnectionString = hostContext.Configuration.GetConnectionString("seq");

        if (string.IsNullOrEmpty(seqConnectionString))
        {
            throw new ConfigurationException("Seq connection string is required.");
        }

        loggerConfiguration
            .WriteTo.Console()
            .WriteTo.Seq(seqConnectionString)
            .Enrich.WithProperty("Context", "Consumer")
            .Enrich.FromLogContext();
    })
    .ConfigureServices((hostContext, services) =>
    {
        Log.Information("Logging configuration applied.");

        services.AddMassTransit(options =>
        {
            options.AddConsumer<SubmitOrderConsumer>();

            options.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqConnectionString = hostContext.Configuration.GetConnectionString("rabbitmq");

                if (string.IsNullOrEmpty(rabbitMqConnectionString))
                {
                    throw new ConfigurationException("RabbitMq connection string is required.");
                }

                cfg.Host(rabbitMqConnectionString);

                cfg.ReceiveEndpoint("example-rabbitmq-queue-name", e =>
                {
                    e.ConfigureConsumer<SubmitOrderConsumer>(context);
                });
            });
        });
    })
    .Build();

Log.Information("Consumer Running.");

await host.RunAsync();
