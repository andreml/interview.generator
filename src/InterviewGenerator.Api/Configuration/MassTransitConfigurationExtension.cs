using MassTransit;

namespace InterviewGenerator.Api.Configuration;

public static class MassTransitConfigurationExtension
{
    public static void AddMassTransitConfigSender(this IServiceCollection services, IConfiguration configuration)
    {
        var servidor = configuration.GetSection("MassTransit")["Servidor"];
        var usuario = configuration.GetSection("MassTransit")["Usuario"];
        var senha = configuration.GetSection("MassTransit")["Senha"];

        services.AddMassTransit((x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(servidor, "/", h =>
                {
                    h.Username(usuario);
                    h.Password(senha);
                });

                cfg.ConfigureEndpoints(context);
            });
        }));
    }
}
