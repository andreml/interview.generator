using FluentValidation;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.domain.Entidade
{
    public class TipoQuestionario : EntidadeBase
    {
        public string Descricao { get; set; }

        public TipoQuestionario()
        {
        }
    }

    public class TipoQuestionarioValidator : AbstractValidator<TipoQuestionario>
    {
        public TipoQuestionarioValidator()
        {
            RuleFor(x => x.Descricao).NotNull().WithMessage("Descrição é obrigatório");
        }
    }
}
