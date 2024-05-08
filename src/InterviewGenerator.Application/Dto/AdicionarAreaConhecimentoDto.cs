using FluentValidation;
using System.Text.Json.Serialization;

namespace InterviewGenerator.Application.Dto;

public class AdicionarAreaConhecimentoDto
{
    [JsonIgnore]
    public Guid UsuarioId { get; set; }
    public string Descricao { get; set; } = default!;
}

public class AdicionarAreaConhecimentoDtoValidator : AbstractValidator<AdicionarAreaConhecimentoDto>
{
    public AdicionarAreaConhecimentoDtoValidator()
    {
        RuleFor(x => x.Descricao)
            .NotNull().NotEmpty().WithMessage("Descrição é obrigatória")
            .MinimumLength(3).WithMessage("Descrição deve ter no mínimo 3 caracteres")
            .MaximumLength(100).WithMessage("Descrição deve ter até 100 caracteres");
    }
}
