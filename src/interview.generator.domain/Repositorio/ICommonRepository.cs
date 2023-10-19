namespace interview.generator.domain.Repositorio
{
    public interface ICommonRepository<T>
    {
        Task<IEnumerable<T>> ObterTodos();
        Task<T> ObterPorId(Guid id);
        Task Excluir(Guid id);
        Task Adicionar(T entity);
        Task Alterar(T entity);
    }
}
