using FluentValidation;
using interview.generator.domain.Utils;
using System.Text.Json.Serialization;

namespace interview.generator.application.Dto
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
                .MaximumLength(30)
                .WithMessage("Login deve ter até 30 caracteres");

            RuleFor(x => x.Senha)
                .NotNull()
                .NotEmpty()
                .WithMessage("Senha é obrigatória");
        }
    }
}
