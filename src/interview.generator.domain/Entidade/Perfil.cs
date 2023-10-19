using FluentValidation;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class Perfil : EntidadeBase
    {
        public string Descricao { get; set; }

        public Perfil()
        {   
        }
    }

    public class PerfilValidator : AbstractValidator<Perfil>
    {
        public PerfilValidator()
        {
            RuleFor(x => x.Descricao).NotNull().WithMessage("Descrição é obrigatório");
        }
    }
}