using Example.MassTransit.Producer;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            .Enrich.WithProperty("Context", "Producer")
            .Enrich.FromLogContext();
    })
    .ConfigureServices((hostContext, services) =>
    {
        Log.Information("Logging configuration applied.");

        services.AddMassTransit(options =>
        {
            options.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqConnectionString = hostContext.Configuration.GetConnectionString("rabbitmq");

                if (string.IsNullOrEmpty(rabbitMqConnectionString))
                {
                    throw new ConfigurationException("RabbitMq connection string is required.");
                }

                cfg.Host(rabbitMqConnectionString);
            });
        });

        services.AddHostedService<Worker>();
    })
    .Build();

Log.Information("Producer Running.");

await host.RunAsync();
