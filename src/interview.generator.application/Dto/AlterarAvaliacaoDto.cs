using FluentValidation;
using interview.generator.domain.Entidade;

namespace interview.generator.application.Dto
{
    public class AlterarAvaliacaoDto
    {
        public Guid CandidatoId { get; set; }
        public Guid QuestionarioId { get; set; }
        public DateTime DataAplicacao { get; set; } = DateTime.Now;
        public string ObservacaoAplicador { get; set; } = default!;
        public ICollection<RespostaAvaliacao> Respostas { get; set; }
    }

    public class AlterarAvaliacaoDtoValidator : AbstractValidator<AlterarAvaliacaoDto>
    {
        public AlterarAvaliacaoDtoValidator()
        {
            RuleFor(x => x.CandidatoId).NotNull().NotEmpty().WithMessage("Candidato é obrigatório");
            RuleFor(x => x.QuestionarioId).NotNull().NotEmpty().WithMessage("Questionário é obrigatório");
            RuleFor(x => x.Respostas).NotNull().WithMessage("Respostas são obrigatórias é obrigatório");
        }
    }
}
