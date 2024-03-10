using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Domain.Enum;
using MassTransit;

namespace InterviewGenerator.Worker.Consumer
{
    public class EventoImportacaoPerguntasConsumer : IConsumer<ImportarArquivoDto>
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

        public async Task Consume(ConsumeContext<ImportarArquivoDto> context)
        {
            _logger.LogInformation($"Arquivo IdControleImportacao: {context.Message.IdArquivo}");
            var alterarLinhaDto = new AlterarLinhaArquivoDto
            {
                DataProcessamento = DateTime.Now,
                NumeroLinha = context.Message.Pergunta.NumeroLinha,
                IdControleImportacao = context.Message.IdArquivo
            };      

            try
            {
                var validation = new ImportarArquivoDtoValidator().Validate(context.Message);

                if (validation.IsValid)
                {
                    var result = await _perguntaService.CadastrarPergunta(context.Message.Pergunta);

                    if (result.HasError)
                    {
                        _logger.LogInformation($"Erro ao importar a linha {context.Message.Pergunta.NumeroLinha}");
                        alterarLinhaDto.Erro = string.Join("; ", result.Erros);
                        alterarLinhaDto.StatusImportacao = StatusLinhaArquivo.Erro;
                    }
                    else
                    {
                        _logger.LogInformation($"Linha {context.Message.Pergunta.NumeroLinha} importada!");
                        alterarLinhaDto.StatusImportacao = StatusLinhaArquivo.Concluida;
                    }
                }
                else
                {
                    _logger.LogInformation($"Pergunta na linha {context.Message.Pergunta.NumeroLinha} não foi validada!");
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

            _logger.LogInformation($"Finalizando processo atualizando linha {context.Message.Pergunta.NumeroLinha}");
            await _importacaoPerguntaService.AtualizaLinhasArquivo(alterarLinhaDto);
        }
    }
}
    