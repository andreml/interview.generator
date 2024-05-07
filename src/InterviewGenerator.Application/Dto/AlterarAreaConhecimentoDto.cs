using FluentValidation;
using System.Text.Json.Serialization;

namespace InterviewGenerator.Application.Dto;

public class AlterarAreaConhecimentoDto
{
    [JsonIgnore]
    public Guid UsuarioId { get; set; }
    public Guid Id { get; set; }
    public string Descricao { get; set; } = default!;
}

public class AlterarAreaConhecimentoDtoValidator : AbstractValidator<AlterarAreaConhecimentoDto>
{
    public AlterarAreaConhecimentoDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().NotEmpty().WithMessage("Id é obrigatório");

        RuleFor(x => x.Descricao)
            .NotNull().NotEmpty().WithMessage("Descrição é obrigatória")
            .MinimumLength(3).WithMessage("Descrição deve ter no mínimo 3 caracteres")
            .MaximumLength(100).WithMessage("Descrição deve ter até 100 caracteres");
    }
}
