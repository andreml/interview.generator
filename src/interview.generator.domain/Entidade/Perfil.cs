using FluentValidation;

namespace interview.generator.domain.Entidade
{
    public class Perfil : interview.generator.domain.Entidade.Common.Entidade
    {
        public string Descricao { get; set; }
    }

    public class PerfilValidator : AbstractValidator<Perfil>
    {
        public PerfilValidator()
        {
            RuleFor(x => x.Descricao).NotNull().WithMessage("Descrição é obrigatório");
        }
    }
}