using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Eventos;
using InterviewGenerator.Application.Interfaces;
using MassTransit;

namespace InterviewGenerator.Worker.Consumer;

public class EventoImportacaoErro : IConsumer<ImportarPerguntaAsyncEvent>
{
    private readonly IImportacaoPerguntasService _importacaoPerguntaService;
    private readonly ILogger<Worker> _logger;

    public EventoImportacaoErro(
                IImportacaoPerguntasService importacaoPerguntaService,
                ILogger<Worker> logger)
    {
        _importacaoPerguntaService = importacaoPerguntaService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ImportarPerguntaAsyncEvent> context)
    {
        _logger.LogInformation($"Arquivo IdControleImportacao: {context.Message.IdArquivo}");

        _logger.LogInformation($"Arquivo IdControleImportacao: {context.Message.IdArquivo}");
        var alterarLinhaDto = new AlterarLinhaArquivoDto
        {
            DataProcessamento = DateTime.Now,
            NumeroLinha = context.Message.NumeroLinha,
            IdControleImportacao = context.Message.IdArquivo
        };

        _logger.LogInformation($"Finalizando processo atualizando linha {context.Message.NumeroLinha}");
        await _importacaoPerguntaService.AtualizaLinhasArquivo(alterarLinhaDto);
    }
}
