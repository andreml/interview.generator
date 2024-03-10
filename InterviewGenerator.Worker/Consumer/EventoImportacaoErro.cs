using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Domain.Enum;
using MassTransit;

namespace InterviewGenerator.Worker.Consumer
{
    public class EventoImportacaoErro : IConsumer<ImportarArquivoDto>
    {
        private readonly IPerguntaService _perguntaService;
        private readonly IImportacaoPerguntasService _importacaoPerguntaService;
        private readonly ILogger<Worker> _logger;

        public EventoImportacaoErro(IPerguntaService perguntaService, 
                                                 IImportacaoPerguntasService importacaoPerguntaService,
                                                 ILogger<Worker> logger)
        {
            _perguntaService = perguntaService;
            _importacaoPerguntaService = importacaoPerguntaService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ImportarArquivoDto> context)
        {
            _logger.LogInformation($"Arquivo IdControleImportacao: {context.Message.IdArquivo}");

            _logger.LogInformation($"Arquivo IdControleImportacao: {context.Message.IdArquivo}");
            var alterarLinhaDto = new AlterarLinhaArquivoDto
            {
                DataProcessamento = DateTime.Now,
                NumeroLinha = context.Message.Pergunta.NumeroLinha,
                IdControleImportacao = context.Message.IdArquivo
            };

            _logger.LogInformation($"Finalizando processo atualizando linha {context.Message.Pergunta.NumeroLinha}");
            await _importacaoPerguntaService.AtualizaLinhasArquivo(alterarLinhaDto);
        }
    }
}
    