using FluentValidation;
using System.Text.Json.Serialization;

namespace interview.generator.application.Dto
{
    public class AdicionarObservacaoAvaliadorDto
    {
        [JsonIgnore]
        public Guid UsuarioIdCriacaoQuestionario { get; set; }
        public Guid AvaliacaoId { get; set; }
        public string ObservacaoAvaliador { get; set; } = default!;

        public class AdicionarObservacaoAvaliadorDtoValidator : AbstractValidator<AdicionarObservacaoAvaliadorDto>
        {
            public AdicionarObservacaoAvaliadorDtoValidator()
            {
                RuleFor(x => x.AvaliacaoId).NotNull().NotEmpty().WithMessage("Id da Avaliação é obrigatório");
                RuleFor(x => x.ObservacaoAvaliador).MaximumLength(500).WithMessage("Tamanho do campo observação é de no máximo 500 caracteres");
            }
        }
    }
}
