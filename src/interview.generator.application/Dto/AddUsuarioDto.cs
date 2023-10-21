using FluentValidation;
using interview.generator.domain.Enum;
using interview.generator.domain.Utils;
using System.Net.Mail;

namespace interview.generator.application.Dto
{
    public class AddUsuarioDto
    {
        public AddUsuarioDto(string cpf, string nome, Perfil perfil, string login, string senha)
        {
            Cpf = cpf;
            Nome = nome;
            Perfil = perfil;
            Login = login;
            Senha = senha;
        }

        public string Cpf { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public Perfil Perfil { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }

    public class AddUsuarioDtoValidator : AbstractValidator<AddUsuarioDto>
    {
        public AddUsuarioDtoValidator()
        {
            RuleFor(x => x.Cpf).NotNull().WithMessage("Cpf é obrigatório");
            RuleFor(x => x.Cpf).Must(document => ValidateDocument.IsCpf(document)).WithMessage("Documento inválido");
            RuleFor(x => x.Nome).NotNull().WithMessage("Nome é obrigatório");
        }
    }
}
