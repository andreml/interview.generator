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

        public EventoImportacaoPerguntasConsumer(IPerguntaService perguntaService, IImportacaoPerguntasService importacaoPerguntaService)
        {
            _perguntaService = perguntaService;
            _importacaoPerguntaService = importacaoPerguntaService;
        }

        public async Task Consume(ConsumeContext<ImportarArquivoDto> context)
        {
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
                        alterarLinhaDto.Erro = string.Join("; ", result.Erros);
                        alterarLinhaDto.StatusImportacao = StatusLinhaArquivo.Erro;
                    }
                    else
                    {
                        alterarLinhaDto.StatusImportacao = StatusLinhaArquivo.Concluida;
                    }
                }
                else
                {
                    alterarLinhaDto.Erro = string.Join("; ", validation.Errors);
                    alterarLinhaDto.StatusImportacao = StatusLinhaArquivo.Erro;
                }
            }
            catch (Exception)
            {
                alterarLinhaDto.Erro = "Erro inesperado";
                alterarLinhaDto.StatusImportacao = StatusLinhaArquivo.Erro;
            }

            await _importacaoPerguntaService.AtualizaLinhasArquivo(alterarLinhaDto);
        }
    }
}
    