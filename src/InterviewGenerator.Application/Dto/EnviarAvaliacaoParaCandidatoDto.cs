using FluentValidation;
using InterviewGenerator.Domain.Utils;
using System.Text.Json.Serialization;

namespace InterviewGenerator.Application.Dto;

public class EnviarAvaliacaoParaCandidatoDto
{
    [JsonIgnore]
    public Guid UsuarioId { get; set; }
    public string LoginCandidato { get; set; } = default!;
    public Guid QuestionarioId { get; set; }
}

public class EnviarAvaliacaoParaCandidatoDtoValidator : AbstractValidator<EnviarAvaliacaoParaCandidatoDto>
{
    public EnviarAvaliacaoParaCandidatoDtoValidator()
    {
        RuleFor(x => x.LoginCandidato)
            .Length(5,30).WithMessage("Login do Candidato deve ter entre 5 e 30 caracteres")
            .Matches(RegexUtils.LoginValidador).WithMessage("Login deve conter apenas lenhas, pontos e underlines");

        RuleFor(x => x.QuestionarioId)
            .NotEmpty().WithMessage("Id do questionário é obrigatório");
    }
}

