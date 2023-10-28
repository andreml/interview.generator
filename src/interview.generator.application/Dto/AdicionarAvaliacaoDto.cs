using FluentValidation;
using System.Text.Json.Serialization;

namespace interview.generator.application.Dto
{
    public class AdicionarAvaliacaoDto
    {
        [JsonIgnore]
        public Guid CandidatoId { get; set; }
        public Guid QuestionarioId { get; set; }
        public ICollection<RespostaAvaliacaoDto> Respostas { get; set; } = default!;
    }

    public class RespostaAvaliacaoDto
    {
        public RespostaAvaliacaoDto(Guid perguntaId, Guid alternativaId)
        {
            PerguntaId = perguntaId;
            AlternativaId = alternativaId;
        }

        public Guid PerguntaId { get; set; }
        public Guid AlternativaId { get; set; }
    }

    public class AdicionarAvaliacaoDtoValidator : AbstractValidator<AdicionarAvaliacaoDto>
    {
        public AdicionarAvaliacaoDtoValidator()
        {
            RuleFor(x => x.QuestionarioId)
                .NotNull().NotEmpty().WithMessage("Questionário é obrigatório");

            RuleFor(x => x.Respostas)
                .NotNull().NotEmpty().WithMessage("Respostas são obrigatórias")
                .Must(x => x.Select(x => x.PerguntaId).Count() == x.Select(x => x.PerguntaId).Distinct().Count())
                    .WithMessage("Uma ou mais respostas estão duplicadas");
        }
    }
}
