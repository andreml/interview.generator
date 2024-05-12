using FluentValidation;
using System.Text.Json.Serialization;

namespace InterviewGenerator.Application.Dto;

public class ResponderAvaliacaoDto
{
    [JsonIgnore]
    public Guid CandidatoId { get; set; }
    public Guid AvaliacaoId { get; set; }
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

public class ResponderAvaliacaoDtoValidator : AbstractValidator<ResponderAvaliacaoDto>
{
    public ResponderAvaliacaoDtoValidator()
    {
        RuleFor(x => x.AvaliacaoId)
            .NotNull().NotEmpty().WithMessage("AvaliaçãoId é obrigatório");

        RuleFor(x => x.Respostas)
            .NotNull().NotEmpty().WithMessage("Respostas são obrigatórias")
            .Must(x => x.Select(x => x.PerguntaId).Count() == x.Select(x => x.PerguntaId).Distinct().Count())
                .WithMessage("Uma ou mais respostas estão duplicadas");
    }
}
