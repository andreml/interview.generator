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
        private readonly ILinhasArquivoRepositorio _linhasArquivoRepositorio;

        public ImportacaoPerguntaService(IControleImportacaoPerguntasRepositorio controleImportacaoRepositorio,
                                         IMassTransitService massTransitService,
                                         ILinhasArquivoRepositorio linhasArquivoRepositorio)
        {
            _controleImportacaoRepositorio = controleImportacaoRepositorio;
            _massTransitService = massTransitService;
            _linhasArquivoRepositorio = linhasArquivoRepositorio;
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
            List<AdicionarPerguntaDto> perguntas = new();
            try
            {
                perguntas = File.ReadAllLines(filePath)
                                           .Skip(1)
                                           .Select(v => AdicionarPerguntaDto.FromCsv(v))
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

            var statusImportacao = new Domain.Entidade.ControleImportacaoPerguntas
            {
                StatusImportacao = Domain.Enum.StatusImportacao.Pendente,
                DataUpload = DateTime.Now,
                Id = Guid.NewGuid(),
                NomeArquivo = filePath,
                UsuarioId = usuarioId,
                QuantidadeLinhasImportadas = perguntas.Count
            };

            await _controleImportacaoRepositorio.Adicionar(statusImportacao);

            for (int i = 0; i < perguntas.Count; i++)
            {
                var mensagem = new ImportarArquivoDto { NumeroLinha = i+1, Pergunta = perguntas[i] };
                await _massTransitService.InserirMensagem(mensagem, "teste");
                await _linhasArquivoRepositorio.Adicionar(new Domain.Entidade.LinhasArquivo
                {
                    IdControleImportacao = statusImportacao.Id,
                    NumeroLinha = mensagem.NumeroLinha
                });
            }

            return response;
        }
    }
}
