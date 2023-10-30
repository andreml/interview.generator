using FluentValidation;
using InterviewGenerator.Domain.Enum;
using InterviewGenerator.Domain.Utils;

namespace InterviewGenerator.Application.Dto
{
    public class AdicionarUsuarioDto
    {
        public AdicionarUsuarioDto(string cpf, string nome, Perfil perfil, string login, string senha)
        {
            Cpf = cpf;
            Nome = nome;
            Perfil = perfil;
            Login = login;
            Senha = senha;
        }

        public string Cpf { get; set; }
        public string Nome { get; set; }
        public Perfil Perfil { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }

    public class AdicionarUsuarioDtoValidator : AbstractValidator<AdicionarUsuarioDto>
    {
        public AdicionarUsuarioDtoValidator()
        {
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
