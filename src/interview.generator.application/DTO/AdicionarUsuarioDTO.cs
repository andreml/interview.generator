using FluentValidation;
using interview.generator.domain.Enums;
using interview.generator.domain.Utils;

namespace interview.generator.application.DTO
{
    public class AdicionarUsuarioDTO
    {
        public string Cpf { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public Perfil Perfil { get; set; }
    }

    public class AdicionarUsuarioDTOValidator : AbstractValidator<AdicionarUsuarioDTO>
    {
        public AdicionarUsuarioDTOValidator()
        {
            RuleFor(x => x.Cpf).NotNull().WithMessage("Cpf é obrigatório");

            RuleFor(x => x.Cpf).Must(document => ValidateDocument.IsCpf(document)).WithMessage("Cpf inválido");

            RuleFor(x => x.Nome).NotNull().WithMessage("Nome é obrigatório");

            RuleFor(x => x.Perfil).NotNull().WithMessage("Perfil é obrigatório");
        }
    }
}
