namespace interview.generator.domain.Repositorio
{
    public interface ICommonRepository<T>
    {
        Task<IEnumerable<T>> ObterTodos();
        Task<T> ObterPorId(int id);
        Task Excluir(int id);
        Task Adicionar(T entity);
        Task Alterar(T entity);
    }
}
