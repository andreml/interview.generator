using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Repositorio;
using Microsoft.AspNetCore.Http;

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
                ErrosImportacao = c.ErrosImportacao,
            });

            response.AddData(viewModelResponse);

            return response;
        }

        public async Task<ResponseBase<ControleImportacaoPerguntasViewModel>> ImportarArquivoPerguntas(IFormFile arquivo, Guid usuarioId)
        {
            var response = new ResponseBase<ControleImportacaoPerguntasViewModel>();

            List<AdicionarPerguntaDto> perguntas = new();

            var idImportacao = Guid.NewGuid();

            try
            {
                using var streamReader = new StreamReader(arquivo.OpenReadStream());
                string cabecalho = streamReader.ReadLine()!;
                while (!streamReader.EndOfStream)
                {
                    string linha = streamReader.ReadLine()!;
                    perguntas.Add(AdicionarPerguntaDto.FromCsv(linha));
                }

                for (int i = 0; i < perguntas.Count; i++)
                {
                    perguntas[i].NumeroLinha = i + 1;
                    perguntas[i].UsuarioId = usuarioId;
                }
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


            var controleImportacao = new ControleImportacaoPerguntas
            {
                StatusImportacao = Domain.Enum.StatusImportacao.Pendente,
                DataUpload = DateTime.Now,
                Id = idImportacao,
                NomeArquivo = arquivo.FileName,
                UsuarioId = usuarioId,
                QuantidadeLinhasImportadas = perguntas.Count,
                LinhasArquivo = perguntas
                                    .Select(p => new LinhasArquivo { IdControleImportacao = idImportacao, NumeroLinha = p.NumeroLinha})
                                    .ToList()
            };

            // Envia mensagens de cadastro
            for (int i = 0; i < perguntas.Count; i++)
            {
                var mensagem = new ImportarArquivoDto { Pergunta = perguntas[i], IdArquivo = controleImportacao.Id };
                await _massTransitService.InserirMensagem(mensagem, "importacao-perguntas-async");
            }

            await _controleImportacaoRepositorio.Adicionar(controleImportacao);

            response.AddData(new ControleImportacaoPerguntasViewModel
            {
                StatusImportacao = controleImportacao.StatusImportacao,
                DataUpload = controleImportacao.DataUpload,
                NomeArquivo = controleImportacao.NomeArquivo,
                QuantidadeLinhasImportadas = controleImportacao.QuantidadeLinhasImportadas,
                UsuarioId = controleImportacao.UsuarioId,
                IdArquivo = controleImportacao.Id
            });

            return response;
        }

        public async Task<ResponseBase> AtualizaLinhasArquivo(AlterarLinhaArquivoDto alterarLinhaArquivoDto)
        {
            var response = new ResponseBase();

            var linha = await _linhasArquivoRepositorio.ObterLinhaArquivo(alterarLinhaArquivoDto.IdControleImportacao, alterarLinhaArquivoDto.NumeroLinha);

            if (linha == null)
            {
                response.AddErro("Linha não encontrada");
                return response;
            }

            linha.Erro = alterarLinhaArquivoDto.Erro;
            linha.DataProcessamento = alterarLinhaArquivoDto.DataProcessamento;

            await _linhasArquivoRepositorio.Alterar(linha);

            response.AddData("Linha do arquivo alterada com sucesso!");
            return response;
        }
    }
}
