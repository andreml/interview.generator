using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace interview.generator.api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de Questionários (Conjunto de perguntas)
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class QuestionarioController : BaseController
    {
        readonly IQuestionarioService _questionarioService;

        public QuestionarioController(IQuestionarioService questionarioService)
        {
            _questionarioService = questionarioService;
        }

        /// <summary>
        /// Adiciona um novo questionário (Avaliador)
        /// </summary>
        [HttpPost("AdicionarQuestionario")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarQuestionario(AdicionarQuestionarioDto questionario)
        {
            try
            {
                questionario.UsuarioId = ObterUsuarioIdLogado();
                var result = await _questionarioService.CadastrarQuestionario(questionario);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao incluir questionário");
            }
        }


        /// <summary>
        /// Altera um questionário existente (Avaliador)
        /// </summary>
        /// <param name="questionario"></param>
        [HttpPut("AlterarQuestionario")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarQuestionario(AlterarQuestionarioDto questionario)
        {
            try
            {
                questionario.UsuarioId = ObterUsuarioIdLogado();
                var result = await _questionarioService.AlterarQuestionario(questionario);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao alterar questionário");
            }
        }

        /// <summary>
        /// Exclui um  um questionário existente (Avaliador)
        /// </summary>
        [HttpDelete("ExcluirQuestionario/{id}")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExcluirQuestionario(Guid id)
        {
            try
            {
                var result = await _questionarioService.ExcluirQuestionario(ObterUsuarioIdLogado(), id);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao excluir questionario");
            }
        }

        /// <summary>
        /// Obém questionários cadastrados (Avaliador)
        /// </summary>
        /// <param name="questionarioId">Id do questionário (opcional)</param>
        /// <param name="nome">Nome do questionário (opcional)</param>
        [HttpGet("ObterQuestionarios")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(typeof(ICollection<QuestionarioViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterQuestionariosPorFiltro([FromQuery] Guid questionarioId, [FromQuery] string? nome)
        {
            try
            {
                var result =  await _questionarioService.ObterQuestionarios(ObterUsuarioIdLogado(), questionarioId, nome);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao obter questionários");
            }
        }

    }
}
