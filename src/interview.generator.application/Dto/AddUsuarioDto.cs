using FluentValidation;
using interview.generator.domain.Enum;
using interview.generator.domain.Utils;

namespace interview.generator.application.Dto
{
    public class AddUsuarioDto
    {
        public AddUsuarioDto(string cpf, string nome, Perfil perfil)
        {
            Cpf = cpf;
            Nome = nome;
            Perfil = perfil;
        }

        public string Cpf { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public Perfil Perfil { get; set; }
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
