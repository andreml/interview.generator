using FluentValidation;
using interview.generator.domain.Entidade;

namespace interview.generator.application.Dto
{
    public class AdicionarAvaliacaoDto
    {
        public AdicionarAvaliacaoDto(Guid candidatoId, Guid questionarioId, ICollection<RespostaAvaliacao> respostas)
        {
            CandidatoId = candidatoId;
            QuestionarioId = questionarioId;
            Respostas = respostas;
        }

        public Guid CandidatoId { get; set; }
        public Guid QuestionarioId { get; set; }
        public DateTime DataAplicacao { get; set; } = DateTime.Now;
        public ICollection<RespostaAvaliacao> Respostas { get; set; }
    }

    public class AdicionarAvaliacaoDtoValidator : AbstractValidator<AdicionarAvaliacaoDto>
    {
        public AdicionarAvaliacaoDtoValidator()
        {
            RuleFor(x => x.CandidatoId).NotNull().NotEmpty().WithMessage("Candidato é obrigatório");
            RuleFor(x => x.QuestionarioId).NotNull().NotEmpty().WithMessage("Questionário é obrigatório");
            RuleFor(x => x.Respostas).NotNull().WithMessage("Respostas são obrigatórias é obrigatório");
        }
    }
}
