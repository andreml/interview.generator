using InterviewGenerator.CrossCutting.Eventos;
using MassTransit;

namespace InterviewGenerator.Worker.Consumer
{
    public class EventoImportacaoPerguntasConsumer : IConsumer<EventoImportacaoPerguntas>
    {
        public Task Consume(ConsumeContext<EventoImportacaoPerguntas> context)
        {
            return Task.CompletedTask;
        }
    }
}
