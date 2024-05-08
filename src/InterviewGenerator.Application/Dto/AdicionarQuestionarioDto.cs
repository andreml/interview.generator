using FluentValidation;
using System.Text.Json.Serialization;

namespace InterviewGenerator.Application.Dto;

public class AdicionarQuestionarioDto
{
    [JsonIgnore]
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; } = default!;
    public ICollection<Guid> Perguntas { get; set; } = default!;
}

public class AdicionarQuestionarioDtoValidator : AbstractValidator<AdicionarQuestionarioDto>
{
    public AdicionarQuestionarioDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotNull().NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(5).WithMessage("Nome deve conter no minimo 5 caracteres")
            .MaximumLength(200).WithMessage("Nome deve conter no máximo 200 caracteres");

        RuleFor(x => x.Perguntas)
            .NotNull().NotEmpty().WithMessage("Perguntas são obrigatórias")
            .Must(x => x.Count >= 3).WithMessage("O questionário deve ter no mínimo 3 perguntas")
            .Must(x => x.Count <= 50).WithMessage("O questionário deve ter no máximo 50 perguntas")
            .Must(x => x.Count == x.Distinct().Count()).WithMessage("Uma ou mais perguntas estão duplicadas");
    }
}
