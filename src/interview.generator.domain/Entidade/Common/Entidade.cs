namespace interview.generator.domain.Entidade.Common
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; set; }

        protected EntidadeBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
