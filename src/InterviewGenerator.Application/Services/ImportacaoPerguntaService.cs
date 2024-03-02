using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Repositorio;
using System.Text.Json;

namespace InterviewGenerator.Application.Services
{
    public class ImportacaoPerguntaService : IImportacaoPerguntasService
    {
        private readonly IControleImportacaoPerguntasRepositorio _controleImportacaoRepositorio;
        private readonly IMassTransitService _massTransitService;

        public ImportacaoPerguntaService(IControleImportacaoPerguntasRepositorio controleImportacaoRepositorio, IMassTransitService massTransitService)
        {
            _controleImportacaoRepositorio = controleImportacaoRepositorio;
            _massTransitService = massTransitService;
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

        public async Task<ResponseBase> ImportarArquivoPerguntas(string filePath, Guid usuarioId)
        {
            var response = new ResponseBase();
            List<AdicionarPerguntaDto> perguntas = new List<AdicionarPerguntaDto>();
            try
            {
                perguntas = File.ReadAllLines(filePath)
                                           .Skip(1)
                                           .Select(v => AdicionarPerguntaDto.FromCsv(v, usuarioId))
                                           .ToList();
            }
            catch (Exception ex)
            {
                response.AddErro($"Não foi possível ler o arquivo csv: {ex.Message}");
                return response;
            }

            if (perguntas.Count == 0)
            {
                response.AddErro("Não há perguntas a serem incluídas");
                return response;
            }


            foreach (var pergunta in perguntas)
            {
                await _massTransitService.InserirMensagem(pergunta, "teste");
            }

            return response;
        }
    }
}
