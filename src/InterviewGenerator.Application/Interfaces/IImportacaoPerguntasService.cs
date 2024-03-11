using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;
using Microsoft.AspNetCore.Http;

namespace InterviewGenerator.Application.Interfaces
{
    public interface IImportacaoPerguntasService
    {
        Task<ResponseBase<IEnumerable<ControleImportacaoPerguntasViewModel>>> ListarControlesImportacao(Guid usuarioId);
        Task<ResponseBase<ArquivoEmProcessamentoViewModel>> ImportarArquivoPerguntas(IFormFile arquivo, Guid usuarioId);
        Task<ResponseBase> AtualizaLinhasArquivo(AlterarLinhaArquivoDto linhasArquivoViewModel);
    }
}
