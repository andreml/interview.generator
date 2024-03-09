using FluentValidation;

namespace InterviewGenerator.Application.Dto
{
    public class ImportarArquivoDto
    {
        public AdicionarPerguntaDto Pergunta { get; set; } = default!;
        public Guid IdArquivo { get; set; }
    }

    public class ImportarArquivoDtoValidator : AbstractValidator<ImportarArquivoDto>
    {
        public ImportarArquivoDtoValidator()
        {
            RuleFor(x => x.IdArquivo)
                .NotNull().WithMessage("IdArquivo é obrigatório")
                .NotEmpty().WithMessage("IdArquivo é obrigatório");

            RuleFor(x => x.Pergunta)
                .SetValidator(new AdicionarPerguntaDtoValidator());
        }
    }
}
