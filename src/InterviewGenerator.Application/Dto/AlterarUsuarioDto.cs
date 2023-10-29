using FluentValidation;
using InterviewGenerator.Domain.Utils;
using System.Text.Json.Serialization;

namespace InterviewGenerator.Application.Dto
{
    public class AlterarUsuarioDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Cpf { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public string Login { get; set; } = default!;
        public string Senha { get; set; } = default!;
    }

    public class AlterarUsuarioDtoValidator : AbstractValidator<AlterarUsuarioDto>
    {
        public AlterarUsuarioDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id é obrigatório");

            RuleFor(x => x.Cpf).NotNull().WithMessage("Cpf é obrigatório");

            RuleFor(x => x.Cpf).Must(ValidateDocument.IsCpf).WithMessage("Documento inválido");

            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome é obrigatório")
                .MaximumLength(30)
                .WithMessage("Login deve ter até 100 caracteres");

            RuleFor(x => x.Login)
                .NotNull()
                .NotEmpty()
                .WithMessage("Login é obrigatório")
                .MinimumLength(5)
                .WithMessage("Login deve ter no mínimo 5 caracters")
                .MaximumLength(30)
                .WithMessage("Login deve ter até 30 caracteres")
                .Matches(RegexUtils.LoginValidador)
                .WithMessage("Login deve conter apenas lenhas, pontos e underlines");

            RuleFor(x => x.Senha)
                .NotNull()
                .NotEmpty()
                .WithMessage("Senha é obrigatória")
                .MinimumLength(8)
                .WithMessage("Senha deve conter no mínimo 8 caracteres")
                .Matches(RegexUtils.SenhaValidator)
                .WithMessage("Senha deve ter pelo menos uma letra maiúscila, uma minúscula, um número e um caractere especial");
        }
    }
}
