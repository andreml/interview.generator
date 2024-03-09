using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.Services;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewGenerator.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de Importações Perguntas via Upload
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ImportacaoPerguntasController : BaseController
    {
        private readonly IImportacaoPerguntasService _importacaoService;

        public ImportacaoPerguntasController(IImportacaoPerguntasService importacaoService)
        {
            _importacaoService = importacaoService;
        }

        /// <summary>
        /// Obtém o status de todas as importações feitas (Avaliador)
        /// </summary>
        [HttpGet()]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(typeof(IEnumerable<ControleImportacaoPerguntasViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ObterImportacoesAsync()
        {
            try
            {
                var result = await _importacaoService.ListarControlesImportacao(ObterUsuarioIdLogado());

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao obter controles de importação");
            }
        }

        /// <summary>
        /// Obtém o arquivo modelo de importação de perguntas (Avaliador)
        /// </summary>
        [HttpGet("arquivoModelo")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterArquivoModeloImportacaoAsync()
        {
            try
            {
                var nomeArquivo = "modelo_importacao_perguntas.csv";

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Arquivos", nomeArquivo);
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(fileBytes, "application/octet-stream", nomeArquivo);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao obter arquivo modelo de importação");
            }
        }

        [HttpPost("ImportarArquivoPerguntas")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ImportarArquivoPerguntas(string filePath)
        {
            try
            {
                var usuarioId = ObterUsuarioIdLogado();

                var result = await _importacaoService.ImportarArquivoPerguntas(filePath, usuarioId);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao inserir perguntas via arquivo");
            }
        }

        [HttpPost("AtualizarControleLinhaArquivo")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarControleLinhaArquivo(AlterarLinhaArquivoDto alterarLinhaArquivoDto)
        {
            try
            {
                var usuarioId = ObterUsuarioIdLogado();

                var result = await _importacaoService.AtualizaLinhasArquivo(alterarLinhaArquivoDto);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao atualizar status da linha do arquivo");
            }
        }
    }
}
