using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Eventos;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Domain.Enum;
using MassTransit;

namespace InterviewGenerator.Worker.Consumer;

public class EventoImportacaoPerguntasConsumer : IConsumer<ImportarPerguntaAsyncEvent>
{
    private readonly IPerguntaService _perguntaService;
    private readonly IImportacaoPerguntasService _importacaoPerguntaService;
    private readonly ILogger<Worker> _logger;

    public EventoImportacaoPerguntasConsumer(IPerguntaService perguntaService, 
                                             IImportacaoPerguntasService importacaoPerguntaService,
                                             ILogger<Worker> logger)
    {
        _perguntaService = perguntaService;
        _importacaoPerguntaService = importacaoPerguntaService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ImportarPerguntaAsyncEvent> context)
    {
        _logger.LogInformation($"Arquivo IdControleImportacao: {context.Message.IdArquivo}");
        var alterarLinhaDto = new AlterarLinhaArquivoDto
        {
            DataProcessamento = DateTime.Now,
            NumeroLinha = context.Message.NumeroLinha,
            IdControleImportacao = context.Message.IdArquivo
        };      

        try
        {
            var validation = new ImportarPerguntaAsyncEventValidator().Validate(context.Message);

            if (validation.IsValid)
            {
                var result = await _perguntaService.CadastrarPergunta(context.Message.IdUsuario, context.Message.Pergunta);

                if (result.HasError)
                {
                    _logger.LogInformation($"Erro ao importar a linha {context.Message.NumeroLinha}");
                    alterarLinhaDto.Erro = string.Join("; ", result.Erros);
                    alterarLinhaDto.StatusImportacao = StatusLinhaArquivo.Erro;
                }
                else
                {
                    _logger.LogInformation($"Linha {context.Message.NumeroLinha} importada!");
                    alterarLinhaDto.StatusImportacao = StatusLinhaArquivo.Concluida;
                }
            }
            else
            {
                _logger.LogInformation($"Pergunta na linha {context.Message.NumeroLinha} não foi validada!");
                alterarLinhaDto.Erro = string.Join("; ", validation.Errors);
                alterarLinhaDto.StatusImportacao = StatusLinhaArquivo.Erro;
            }
        }
        catch (Exception)
        {
            _logger.LogInformation($"Erro inesperado no arquivo Id {context.Message.IdArquivo}");
            alterarLinhaDto.Erro = "Erro inesperado";
            alterarLinhaDto.StatusImportacao = StatusLinhaArquivo.Erro;
        }

        _logger.LogInformation($"Finalizando processo atualizando linha {context.Message.NumeroLinha}");
        await _importacaoPerguntaService.AtualizaLinhasArquivo(alterarLinhaDto);
    }
}
