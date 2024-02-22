using InterviewGenerator.Worker;
using InterviewGenerator.Worker.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();

        services.AddMassTransitConfig(hostContext.Configuration);
    })
    .Build();

host.Run();
