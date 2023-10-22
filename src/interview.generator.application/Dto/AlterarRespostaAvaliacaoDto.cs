using FluentValidation;

namespace interview.generator.application.Dto
{
    public class AlterarRespostaAvaliacaoDto
    {
        public Guid AvaliacaoId { get; set; }
        public Guid PerguntaQuestionarioId { get; set; } = default!;
        public Guid AlternativaEscolhidaId { get; set; } = default!;
    }

    public class AlterarRespostaAvaliacaoDtoValidator : AbstractValidator<AlterarRespostaAvaliacaoDto>
    {
        public AlterarRespostaAvaliacaoDtoValidator()
        {
            RuleFor(x => x.AvaliacaoId).NotNull().NotEmpty().WithMessage("Identificador da Avaliação é obrigatório");
            RuleFor(x => x.PerguntaQuestionarioId).NotNull().NotEmpty().WithMessage("Identificador da Pergunta é obrigatório");
            RuleFor(x => x.AlternativaEscolhidaId).NotNull().NotEmpty().WithMessage("Identificador da Alternativa é obrigatório");
        }
    }
}
