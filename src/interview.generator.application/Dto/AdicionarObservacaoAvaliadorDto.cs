using FluentValidation;

namespace interview.generator.application.Dto
{
    public class AdicionarObservacaoAvaliadorDto
    {
        public Guid CandidatoId { get; set; }
        public Guid QuestionarioId { get; set; }
        public string ObservacaoAvaliador { get; set; }

        public class AdicionarObservacaoAvaliadorDtoValidator : AbstractValidator<AdicionarObservacaoAvaliadorDto>
        {
            public AdicionarObservacaoAvaliadorDtoValidator()
            {
                RuleFor(x => x.CandidatoId).NotNull().NotEmpty().WithMessage("Candidato é obrigatório");
                RuleFor(x => x.QuestionarioId).NotNull().NotEmpty().WithMessage("Questionário é obrigatório");
                RuleFor(x => x.ObservacaoAvaliador).MaximumLength(500).WithMessage("Tamanho do campo observação é de no máximo 500 caracteres");
            }
        }
    }
}
