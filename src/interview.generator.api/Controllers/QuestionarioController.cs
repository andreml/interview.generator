using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.Services;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade;
using interview.generator.domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace interview.generator.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionarioController : BaseController
    {
        readonly IQuestionarioService _questionarioService;

        public QuestionarioController(IQuestionarioService questionarioService)
        {
            _questionarioService = questionarioService;
        }

        [HttpPost("AdicionarQuestionario")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> AdicionarQuestionario(AdicionarQuestionarioDto questionario)
        {
            try
            {
                var usuarioId = ObterUsuarioIdLogado();

                var result = await _questionarioService.CadastrarQuestionario(questionario, usuarioId);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao incluir area do conhecimento");
            }
        }

        [HttpPut("AlterarQuestionario")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> AlterarQuestionario(AlterarQuestionarioDto questionario)
        {
            try
            {
                var usuarioId = ObterUsuarioIdLogado();

                var result = await _questionarioService.AlterarQuestionario(questionario, usuarioId);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao incluir area do conhecimento");
            }
        }

        [HttpDelete("ExcluirQuestionario/{id}")]
        [Authorize(Roles = $"{Perfis.Candidato}")]
        public async Task<IActionResult> ExcluirQuestionario(Guid id)
        {
            try
            {
                var usuarioId = ObterUsuarioIdLogado();

                var result = await _questionarioService.ExcluirQuestionario(id);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao incluir questionario");
            }
        }

        [HttpGet("ObterQuestionarioPorCandidato/")]

        [Authorize(Roles = $"{Perfis.Candidato}")]
        public async Task<IActionResult> ObterQuestionarioPorCandidato()
        {
            try
            {
                var usuarioId = ObterUsuarioIdLogado();

                var result =  _questionarioService.ObterQuestionarioPorCandidato(usuarioId);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao incluir area do conhecimento");
            }
        }

        [HttpGet("ObterQuestionariosPorDescricao/{descricao}")]

        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> ObterQuestionariosPorFiltro(string descricao)
        {
            try
            {
                var usuarioId = ObterUsuarioIdLogado();

                var result =  _questionarioService.ObterQuestionariosPorDescricao(descricao);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao incluir area do conhecimento");
            }
        }

    }
}
