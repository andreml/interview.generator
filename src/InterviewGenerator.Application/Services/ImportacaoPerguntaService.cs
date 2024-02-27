using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Repositorio;

namespace InterviewGenerator.Application.Services
{
    public class ImportacaoPerguntaService : IImportacaoPerguntasService
    {
        private readonly IControleImportacaoPerguntasRepositorio _controleImportacaoRepositorio;

        public ImportacaoPerguntaService(IControleImportacaoPerguntasRepositorio controleImportacaoRepositorio)
        {
            _controleImportacaoRepositorio = controleImportacaoRepositorio;
        }

        public async Task<ResponseBase<IEnumerable<ControleImportacaoPerguntasViewModel>>> ListarControlesImportacao(Guid usuarioId)
        {
            var response = new ResponseBase<IEnumerable<ControleImportacaoPerguntasViewModel>>();

            var controlesImportacao = await _controleImportacaoRepositorio.ObterControlesImportacao(usuarioId);

            if (controlesImportacao.Count() == 0)
                return response;

            var viewModelResponse = controlesImportacao.Select(c => new ControleImportacaoPerguntasViewModel
            {
                DataUpload = c.DataUpload,
                DataFimImportacao = c.DataFimImportacao,
                StatusImportacao = c.StatusImportacao,
                NomeArquivo = c.NomeArquivo,
                ErrosImportacao = (string.IsNullOrEmpty(c.ErrosImportacao)) ? null : c.ErrosImportacao.Split("; "),
            });

            response.AddData(viewModelResponse);

            return response;
        }
    }
}
