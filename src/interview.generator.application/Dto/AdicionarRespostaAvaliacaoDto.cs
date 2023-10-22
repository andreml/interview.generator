using FluentValidation;

namespace interview.generator.application.Dto
{
    public class AdicionarRespostaAvaliacaoDto
    {
        public AdicionarRespostaAvaliacaoDto(Guid avaliacaoId, Guid perguntaQuestionarioId, Guid alternativaEscolhidaId)
        {
            AvaliacaoId = avaliacaoId;
            PerguntaQuestionarioId = perguntaQuestionarioId;
            AlternativaEscolhidaId = alternativaEscolhidaId;
        }

        public Guid AvaliacaoId { get; set; }
        public Guid PerguntaQuestionarioId { get; set; }
        public Guid AlternativaEscolhidaId { get; set; }
    }

    public class AdicionarRespostaAvaliacaoDtoValidator : AbstractValidator<AdicionarRespostaAvaliacaoDto>
    {
        public AdicionarRespostaAvaliacaoDtoValidator()
        {
            RuleFor(x => x.AvaliacaoId).NotNull().NotEmpty().WithMessage("Identificador da Avaliação é obrigatório");
            RuleFor(x => x.PerguntaQuestionarioId).NotNull().NotEmpty().WithMessage("Identificador da Pergunta é obrigatório");
            RuleFor(x => x.AlternativaEscolhidaId).NotNull().NotEmpty().WithMessage("Identificador da Alternativa é obrigatório");
        }
    }
}
