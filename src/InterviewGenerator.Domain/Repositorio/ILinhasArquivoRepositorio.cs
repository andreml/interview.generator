using InterviewGenerator.Domain.Entidade;

namespace InterviewGenerator.Domain.Repositorio
{
    public interface ILinhasArquivoRepositorio : ICommonRepository<LinhaArquivo>
    {
        Task<LinhaArquivo?> ObterLinhaArquivo(Guid idArquivo, int idLinha);
    }
}
