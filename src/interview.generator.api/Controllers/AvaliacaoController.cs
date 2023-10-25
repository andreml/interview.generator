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
        readonly IAvaliacaoService _;
        public AvaliacaoController(IAvaliacaoService service) { _ = service; }

        /// <summary>
        /// Obtém avaliações cadastradas (Avaliador)
        /// </summary>
        /// <param name="CandidatoId?">Id do Candidato</param>
        /// <param name="QuestionarioId?">Id do Questionário</param>
        [HttpGet("ObterAvaliacoesPorFiltro")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> ObterAvaliacoesPorFiltroAsync(Guid? CandidatoId, Guid? QuestionarioId)
        {
            try
            {
                var result = await _.ObterAvaliacaoPorFiltro(ObterUsuarioIdLogado(), CandidatoId, QuestionarioId);

                return Response(result!);
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
                var result = await _.AdicionarAvaliacao(obj);

                return Response(result!);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao adicionar avaliação");
            }
        }

        /// <summary>
        /// Adiciona uma observação na avaliação do candidato (Avaliador)
        /// </summary>
        [HttpPut("AdicionarObservacaoAvaliador")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> AdicionarObservacaoAvaliacaoAsync(AdicionarObservacaoAvaliadorDto obj)
        {
            try
            {
                obj.UsuarioIdCriacaoQuestionario = ObterUsuarioIdLogado();
                var result = await _.AdicionarObservacaoAvaliacao(obj);

                return Response(result!);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao adicionar observação");
            }
        }
    }
}
