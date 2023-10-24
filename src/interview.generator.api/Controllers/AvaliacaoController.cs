using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
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
        //[Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> ObterAvaliacoesPorFiltroAsync(Guid? CandidatoId, Guid? QuestionarioId)
        {
            try
            {
                var result = await _.ObterAvaliacaoPorFiltro(CandidatoId, QuestionarioId);
                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = e.Message
                });
            }
        }

        /// <summary>
        /// Adiciona a avaliação do candidato (Candidato)
        /// </summary>
        /// <param name="AdicionarAvaliacaoDto">Objeto AdicionarAvaliacaoDto</param>
        [HttpPost("Adicionar")]
        //[Authorize(Roles = $"{Perfis.Candidato}")]
        public async Task<IActionResult> AdicionarAvaliacaoAsync(AdicionarAvaliacaoDto obj)
        {
            try
            {
                var result = await _.AdicionarAvaliacao(obj);
                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = e.Message
                });
            }
        }

        /// <summary>
        /// Adiciona uma observação na avaliação do candidato (Avaliador)
        /// </summary>
        /// <param name="AdicionarAvaliacaoDto">Objeto AdicionarAvaliacaoDto</param>
        [HttpPost("AdicionarObservacaoAvaliador")]
        //[Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> AdicionarObservacaoAvaliacaoAsync(AdicionarObservacaoAvaliadorDto obj)
        {
            try
            {
                var result = await _.AdicionarObservacaoAvaliacao(obj);
                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = e.Message
                });
            }
        }
    }
}