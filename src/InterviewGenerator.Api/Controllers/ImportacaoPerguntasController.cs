using InterviewGenerator.Application.Interfaces;
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
        IImportacaoPerguntasService _importacaoService;

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
    }
}
