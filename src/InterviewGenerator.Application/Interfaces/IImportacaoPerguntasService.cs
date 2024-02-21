using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Application.Interfaces
{
    public interface IImportacaoPerguntasService
    {
        Task<ResponseBase<IEnumerable<ControleImportacaoPerguntasViewModel>>> ListarControlesImportacao(Guid usuarioId);
    }
}
