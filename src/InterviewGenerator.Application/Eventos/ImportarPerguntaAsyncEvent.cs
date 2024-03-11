using FluentValidation;
using InterviewGenerator.Application.Dto;

namespace InterviewGenerator.Application.Eventos
{
    public class ImportarPerguntaAsyncEvent
    {
        public AdicionarPerguntaDto Pergunta { get; set; } = default!;
        public Guid IdArquivo { get; set; }
        public Guid IdUsuario { get; set; }
        public int NumeroLinha { get; set; }

        public static ImportarPerguntaAsyncEvent ConvertFromCsv(string linhaCsv, Guid usuarioId, Guid idArquivo, int numeroLinha)
        {
            string[] values = linhaCsv.Split(';');

            AdicionarPerguntaDto pergunta = new()
            {
                AreaConhecimento = values[0],
                Descricao = values[1],
                Alternativas = new List<AlternativaDto>
                {
                    new(values[2], true),
                    new(values[3], false),
                    new(values[4], false)
                }
            };

            if (!string.IsNullOrEmpty(values[5]))
                pergunta.Alternativas.Add(new(values[5], false));

            if (!string.IsNullOrEmpty(values[6]))
                pergunta.Alternativas.Add(new(values[6], false));

            return new ImportarPerguntaAsyncEvent
            {
                Pergunta = pergunta,
                IdArquivo = idArquivo,
                IdUsuario = usuarioId,
                NumeroLinha = numeroLinha
            };
        }
    }

    public class ImportarPerguntaAsyncEventValidator : AbstractValidator<ImportarPerguntaAsyncEvent>
    {
        public ImportarPerguntaAsyncEventValidator()
        {
            RuleFor(x => x.IdArquivo)
                .NotNull().WithMessage("IdArquivo é obrigatório")
                .NotEmpty().WithMessage("IdArquivo é obrigatório");

            RuleFor(x => x.IdUsuario)
                .NotNull().WithMessage("IdArquivo é obrigatório")
                .NotEmpty().WithMessage("IdArquivo é obrigatório");

            RuleFor(x => x.Pergunta)
                .SetValidator(new AdicionarPerguntaDtoValidator());
        }
    }
}
