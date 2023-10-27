using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace interview.generator.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AvaliacaoController : BaseController
    {
        readonly IAvaliacaoService _avaliacaoService;
        public AvaliacaoController(IAvaliacaoService service) { _avaliacaoService = service; }

        /// <summary>
        /// Obtém avaliações cadastradas (Avaliador)
        /// </summary>
        /// <param name="QuestionarioId">Id do questionário (opcional)</param>
        /// <param name="nomeQuestionario">Nome do questionário (opcional)</param>
        /// <param name="nomeCandidato">Nome do candidato (opcional)</param>
        [HttpGet("ObterAvaliacoesPorFiltro")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> ObterAvaliacoesPorFiltroAsync([FromQuery] Guid QuestionarioId, [FromQuery] string? nomeQuestionario, [FromQuery] string? nomeCandidato)
        {
            try
            {
                var result = await _avaliacaoService.ObterAvaliacoesPorFiltro(ObterUsuarioIdLogado(), QuestionarioId, nomeQuestionario, nomeCandidato);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao obter avaliações");
            }
        }

        /// <summary>
        /// Adiciona a avaliação do candidato (Candidato)
        /// </summary>
        [HttpPost("Adicionar")]
        [Authorize(Roles = $"{Perfis.Candidato}")]
        public async Task<IActionResult> AdicionarAvaliacaoAsync(AdicionarAvaliacaoDto obj)
        {
            try
            {
                obj.CandidatoId = ObterUsuarioIdLogado();
                var result = await _avaliacaoService.AdicionarAvaliacao(obj);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao adicionar avaliação");
            }
        }

        /// <summary>
        /// Adiciona uma observação na avaliação do candidato (Avaliador)
        /// </summary>
        [HttpPut("AdicionarObservacao")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> AdicionarObservacaoAvaliacaoAsync(AdicionarObservacaoAvaliadorDto obj)
        {
            try
            {
                obj.UsuarioIdCriacaoQuestionario = ObterUsuarioIdLogado();
                var result = await _avaliacaoService.AdicionarObservacaoAvaliacao(obj);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao adicionar observação");
            }
        }
    }
}
