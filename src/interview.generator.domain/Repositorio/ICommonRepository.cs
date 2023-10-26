namespace interview.generator.domain.Repositorio
{
    public interface ICommonRepository<T>
    {
        Task Adicionar(T entity);
        Task Alterar(T entity);
    }
}
