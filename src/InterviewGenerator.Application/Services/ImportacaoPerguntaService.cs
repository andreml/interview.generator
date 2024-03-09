using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Enum;
using InterviewGenerator.Domain.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace InterviewGenerator.Application.Services
{
    public class ImportacaoPerguntaService : IImportacaoPerguntasService
    {
        private readonly IControleImportacaoPerguntasRepositorio _controleImportacaoRepositorio;
        private readonly IMassTransitService _massTransitService;
        private readonly ILinhasArquivoRepositorio _linhasArquivoRepositorio;

        private readonly string _nomeFila;

        public ImportacaoPerguntaService(IControleImportacaoPerguntasRepositorio controleImportacaoRepositorio,
                                         IMassTransitService massTransitService,
                                         ILinhasArquivoRepositorio linhasArquivoRepositorio,
                                         IConfiguration configuration)
        {
            _controleImportacaoRepositorio = controleImportacaoRepositorio;
            _massTransitService = massTransitService;
            _linhasArquivoRepositorio = linhasArquivoRepositorio;

            _nomeFila = configuration.GetSection("MassTransit")["NomeFila"]!;
        }

        public async Task<ResponseBase<IEnumerable<ControleImportacaoPerguntasViewModel>>> ListarControlesImportacao(Guid usuarioId)
        {
            var response = new ResponseBase<IEnumerable<ControleImportacaoPerguntasViewModel>>();

            var controlesImportacao = await _controleImportacaoRepositorio.ObterControlesImportacao(usuarioId);

            if (controlesImportacao.Count() == 0)
                return response;

            List<ControleImportacaoPerguntasViewModel> viewModelResponse = new();

            foreach (var controle in controlesImportacao)
            {
                var controleViewModel = new ControleImportacaoPerguntasViewModel
                {
                    IdArquivo = controle.Id,
                    DataUpload = controle.DataUpload,
                    NomeArquivo = controle.NomeArquivo,
                    LinhasArquivo = controle.LinhasArquivo.Select(l => new LinhasArquivoViewModel
                    {
                        DataProcessamento = l.DataProcessamento,
                        Erro = l.Erro,
                        NumeroLinha = l.NumeroLinha,
                        StatusImportacao = l.StatusImportacao
                    }).ToList()
                };

                if (controle.LinhasArquivo.Any(l => l.StatusImportacao == StatusLinhaArquivo.Pendente))
                    controleViewModel.StatusImportacao = StatusImportacao.Pendente;
                else if(controle.LinhasArquivo.Any(l => l.StatusImportacao == StatusLinhaArquivo.Erro))
                    controleViewModel.StatusImportacao = StatusImportacao.ConcluidaComErro;
                else
                    controleViewModel.StatusImportacao = StatusImportacao.Concluida;

                viewModelResponse.Add(controleViewModel);
            }

            response.AddData(viewModelResponse);

            return response;
        }

        public async Task<ResponseBase<ArquivoEmProcessamentoViewModel>> ImportarArquivoPerguntas(IFormFile arquivo, Guid usuarioId)
        {
            var response = new ResponseBase<ArquivoEmProcessamentoViewModel>();

            List<AdicionarPerguntaDto> perguntas = new();

            var idImportacao = Guid.NewGuid();

            try
            {
                using var streamReader = new StreamReader(arquivo.OpenReadStream());
                string cabecalho = streamReader.ReadLine()!;

                int numeroLinha = 1;
                while (!streamReader.EndOfStream)
                {
                    string linha = streamReader.ReadLine()!;
                    perguntas.Add(AdicionarPerguntaDto.FromCsv(linha, usuarioId, numeroLinha++));
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
                DataUpload = DateTime.Now,
                Id = idImportacao,
                NomeArquivo = arquivo.FileName,
                UsuarioId = usuarioId,
                QuantidadeLinhasImportadas = perguntas.Count,
                LinhasArquivo = perguntas
                                    .Select(p => new LinhaArquivo { 
                                                        IdControleImportacao = idImportacao, 
                                                        NumeroLinha = p.NumeroLinha, 
                                                        StatusImportacao = StatusLinhaArquivo.Pendente })
                                    .ToList()
            };

            // Envia mensagens de cadastro
            foreach(var pergunta in perguntas)
            {
                var mensagem = new ImportarArquivoDto { Pergunta = pergunta, IdArquivo = controleImportacao.Id };
                await _massTransitService.InserirMensagem(mensagem, _nomeFila);
            }

            await _controleImportacaoRepositorio.Adicionar(controleImportacao);

            response.AddData(new ArquivoEmProcessamentoViewModel
            {
                NomeArquivo = controleImportacao.NomeArquivo,
                IdArquivo = controleImportacao.Id,
                Linhas = perguntas.Count
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
            linha.StatusImportacao = alterarLinhaArquivoDto.StatusImportacao;

            await _linhasArquivoRepositorio.Alterar(linha);

            response.AddData("Linha do arquivo alterada com sucesso!");
            return response;
        }
    }
}
