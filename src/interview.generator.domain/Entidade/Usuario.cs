using FluentValidation;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Enums;
using interview.generator.domain.Utils;

namespace interview.generator.domain.Entidade
{
    public class Usuario : EntidadeBase
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public Perfil Perfil { get; set; }

        public Usuario()
        {
        }
    }

    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Cpf).NotNull().WithMessage("Cpf é obrigatório");
            RuleFor(x => x.Cpf).Must(document => ValidateDocument.IsCpf(document)).WithMessage("Documento inválido");
            RuleFor(x => x.Nome).NotNull().WithMessage("Nome é obrigatório"); ;
            RuleFor(x => x.Perfil).NotNull().WithMessage("Perfil é obrigatório");
        }
    }
}