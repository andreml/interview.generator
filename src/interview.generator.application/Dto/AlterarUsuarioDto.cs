using FluentValidation;
using interview.generator.domain.Enum;
using interview.generator.domain.Utils;

namespace interview.generator.application.Dto
{
    public class AlterarUsuarioDto
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public Perfil Perfil { get; set; } = default!;
    }

    public class AlterarUsuarioDtoValidator : AbstractValidator<AlterarUsuarioDto>
    {
        public AlterarUsuarioDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id é obrigatório");
            RuleFor(x => x.Cpf).NotNull().WithMessage("Cpf é obrigatório");
            RuleFor(x => x.Cpf).Must(document => ValidateDocument.IsCpf(document)).WithMessage("Documento inválido");
            RuleFor(x => x.Nome).NotNull().WithMessage("Nome é obrigatório");
        }
    }
}
