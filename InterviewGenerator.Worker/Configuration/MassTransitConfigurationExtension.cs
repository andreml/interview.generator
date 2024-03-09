using InterviewGenerator.Worker.Consumer;
using MassTransit;

namespace InterviewGenerator.Worker.Configuration
{
    public static class MassTransitConfigurationExtension
    {
        public static void AddMassTransitConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var servidor = configuration.GetSection("MassTransit")["Servidor"];
            var fila = configuration.GetSection("MassTransit")["NomeFila"];
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

                    cfg.ReceiveEndpoint(fila!, e =>
                    {
                        e.Consumer<EventoImportacaoPerguntasConsumer>();
                        
                    });

                    cfg.ConfigureEndpoints(context);
                });

                x.AddConsumer<EventoImportacaoPerguntasConsumer>();
            }));
        }
    }
}
