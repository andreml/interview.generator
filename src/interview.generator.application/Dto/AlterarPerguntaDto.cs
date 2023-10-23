using FluentValidation;

namespace interview.generator.application.Dto
{
    public class AlterarPerguntaDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = default!;
        public string AreaConhecimento { get; set; } = default!;
        public ICollection<AlterarAlternativaDto> Alternativas { get; set; } = default!;
    }

    public class AlterarAlternativaDto
    {
        public string Descricao { get; set; } = default!;
        public bool Correta { get; set; }
    }

    public class AlterarPerguntaDtoValidator : AbstractValidator<AlterarPerguntaDto>
    {
        public AlterarPerguntaDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty().WithMessage("Id é obrigatório");

            RuleFor(x => x.Descricao)
                .NotNull().NotEmpty().WithMessage("Descricao é obrigatório")
                .MaximumLength(1000).WithMessage("Descrição deve ter até 1000 caracteres");

            RuleFor(x => x.AreaConhecimento)
                .NotNull().NotEmpty()
                .WithMessage("AreaConhecimento é obrigatória")
                .MaximumLength(100).WithMessage("AreaConhecimento deve tera até 100 caracteres");


            RuleFor(x => x.Alternativas)
                .NotNull().NotEmpty()
                .WithMessage("Alternativas são obrigatórias");

            RuleFor(x => x.Alternativas)
                .Must(x => x.Count >= 3 && x.Count <= 5)
                .WithMessage("A pergunda teve ter entre 3 e 5 alternativas");

            RuleFor(x => x.Alternativas)
                .Must(x => x.Where(x => x.Correta).Count() == 1)
                .WithMessage("A pergunda teve ter somente uma alternativa correta");

            RuleFor(x => x.Alternativas)
                .ForEach(alternativa =>
                {
                    RuleFor(alternativa => alternativa.Descricao)
                        .NotNull().NotEmpty().WithMessage("Descrição da alternativa é obrigatória")
                        .MaximumLength(1000).WithMessage("Descrição da alternativa deve ter até 1000 caracteres");
                });
        }
    }
}
