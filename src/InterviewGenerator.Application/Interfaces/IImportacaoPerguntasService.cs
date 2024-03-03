using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Application.Interfaces
{
    public interface IImportacaoPerguntasService
    {
        Task<ResponseBase<IEnumerable<ControleImportacaoPerguntasViewModel>>> ListarControlesImportacao(Guid usuarioId);
        Task<ResponseBase<ControleImportacaoPerguntasViewModel>> ImportarArquivoPerguntas(string filePath, Guid usuarioId);
    }
}
