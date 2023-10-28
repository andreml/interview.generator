using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace interview.generator.api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento das perguntas
    /// </summary>
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
        /// Obtém Perguntas (Avaliador)
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
                var usuarioId = ObterUsuarioIdLogado();

                var result = _perguntaService.ListarPerguntas(usuarioId, perguntaId, areaConhecimento, descricao);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao obter perguntas");
            }
        }

        /// <summary>
        /// Adiciona Pergunta (Avaliador)
        /// </summary>
        [HttpPost("AdicionarPergunta")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarPergunta(AdicionarPerguntaDto pergunta)
        {
            try
            {
                pergunta.UsuarioId = ObterUsuarioIdLogado();

                var result = await _perguntaService.CadastrarPergunta(pergunta);

                return Response(result!);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao adicionar perguntas");
            }
        }

        /// <summary>
        /// Altera uma Pergunta (Avaliador)
        /// </summary>
        [HttpPut("AlterarPergunta")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarPergunta(AlterarPerguntaDto pergunta)
        {
            try
            {
                pergunta.UsuarioId = ObterUsuarioIdLogado();

                var result = await _perguntaService.AlterarPergunta(pergunta);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao alterar pergunta");
            }
        }

        /// <summary>
        /// Exclui uma Pergunta (Avaliador)
        /// </summary>
        [HttpDelete("ExcluirPergunta/{id}")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExcluirPergunta(Guid id)
        {
            try
            {
                var usuarioId = ObterUsuarioIdLogado();

                var result = await _perguntaService.ExcluirPergunta(usuarioId, id);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao excluir pergunta");
            }
        }
    }
}
