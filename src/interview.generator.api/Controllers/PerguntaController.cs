using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace interview.generator.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PerguntaController : BaseController
    {
        readonly IPerguntaService _perguntaService;
        public PerguntaController(IPerguntaService perguntaService)
        {
            _perguntaService = perguntaService;
        }

        /// <summary>
        /// Obtém perguntas cadastradas pelo usuário (Avaliador)
        /// </summary>
        /// <param name="perguntaId">Id da Pergunta (Opcional)</param>
        /// <param name="areaConhecimento">Area de conhecimento (Opcional)</param>
        /// <param name="descricao">Pergunta (Opcional)</param>
        [HttpGet("ObterPerguntas")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(typeof(IEnumerable<PerguntaViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult ObterPerguntas([FromQuery] Guid perguntaId, [FromQuery] string? areaConhecimento, [FromQuery] string? descricao)
        {
            try
            {
                var userId = ObterUsuarioIdLogado();

                var result = _perguntaService.ListarPerguntas(userId, perguntaId, areaConhecimento, descricao);

                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = e.Message,
                    Excecao = "Erro ao obter perguntas"
                });
            }
        }

        /// <summary>
        /// Adiciona nova pergunta
        /// </summary>
        [HttpPost("AdicionarPergunta")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarPergunta(AdicionarPerguntaDto pergunta)
        {
            try
            {
                var usuarioId = ObterUsuarioIdLogado();

                var result = await _perguntaService.CadastrarPergunta(pergunta, usuarioId);

                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = "Erro ao incluir pergunta",
                    Excecao = e.Message
                });
            }
        }
    }
}