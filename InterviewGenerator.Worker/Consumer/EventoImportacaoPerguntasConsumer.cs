using InterviewGenerator.Application.Dto;
using InterviewGenerator.CrossCutting.Eventos;
using MassTransit;

namespace InterviewGenerator.Worker.Consumer
{
    public class EventoImportacaoPerguntasConsumer : IConsumer<ImportarArquivoDto>
    {
        public Task Consume(ConsumeContext<ImportarArquivoDto> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }
    }
}
    