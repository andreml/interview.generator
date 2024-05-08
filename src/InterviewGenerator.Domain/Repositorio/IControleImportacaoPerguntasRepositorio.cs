using InterviewGenerator.Domain.Entidade;

namespace InterviewGenerator.Domain.Repositorio;

public interface IControleImportacaoPerguntasRepositorio : ICommonRepository<ControleImportacaoPerguntas>
{
    Task<IEnumerable<ControleImportacaoPerguntas>> ObterControlesImportacao(Guid usuarioId);
}
