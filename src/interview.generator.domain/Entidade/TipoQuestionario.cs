using FluentValidation;

namespace interview.generator.domain.Entidade
{
    public class TipoQuestionario : Common.Entidade
    {
        public string Descricao { get; set; }
    }

    public class TipoQuestionarioValidator : AbstractValidator<TipoQuestionario>
    {
        public TipoQuestionarioValidator()
        {
            RuleFor(x => x.Descricao).NotNull().WithMessage("Descrição é obrigatório");
        }
    }
}
