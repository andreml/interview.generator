using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
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
            var validation = new ImportarArquivoDtoValidator().Validate(context.Message);

            var alterarLinhaDto = new AlterarLinhaArquivoDto
            {
                DataProcessamento = DateTime.Now,
                NumeroLinha = context.Message.Pergunta.NumeroLinha,
                IdControleImportacao = context.Message.IdArquivo
            };

            if (validation.IsValid)
            {
                var result = await _perguntaService.CadastrarPergunta(context.Message.Pergunta);

                if(result.HasError)
                    alterarLinhaDto.Erro = string.Join("; ", result.Erros);

            }
            else
            {
                alterarLinhaDto.Erro = string.Join("; ", validation.Errors);
            }

            await _importacaoPerguntaService.AtualizaLinhasArquivo(alterarLinhaDto);
        }
    }
}
    