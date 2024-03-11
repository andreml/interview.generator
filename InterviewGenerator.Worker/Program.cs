using InterviewGenerator.CrossCutting.InjecaoDependencia;
using InterviewGenerator.Worker;
using InterviewGenerator.Worker.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();

        // MassTransit
        services.AddMassTransitConfig(hostContext.Configuration);

        // Repository
        services.AddRepository();

        // Services
        services.AddServices();

        //Context
        services.AddContextConfig();

    })
    .Build();

host.Run();
