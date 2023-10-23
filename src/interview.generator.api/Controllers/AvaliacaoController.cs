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

        [HttpGet("ObterPorFiltro")]
        //[Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> ObterRespostaPorFiltroAsync(Guid? CandidatoId, Guid? QuestionarioId, DateTime? DataAplicacao)
        {
            try
            {
                var result = await _.ObterAvaliacaoPorFiltro(CandidatoId, QuestionarioId, DataAplicacao);
                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = e.Message,
                    Excecao = "Erro um erro"
                });
            }
        }

        [HttpPost("Adicionar")]
        //[Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> AdicionarAvaliacao(AdicionarAvaliacaoDto obj)
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
                    Mensagem = e.Message,
                    Excecao = "Erro ao incluir o usuário"
                });
            }
        }

        [HttpPut("Alterar")]
        ///[Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> AlterarAvaliacao(AlterarAvaliacaoDto obj)
        {
            try
            {
                //if (ObterUsuarioIdLogado() != usuario.Id)
                //    return Unauthorized();

                var result = await _.AlterarAvaliacao(obj);
                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = e.Message,
                    Excecao = "Erro ao excluir o usuário"
                });
            }
        }

        [HttpGet("ObterTodas")]
        //[Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> ObterTodasAvaliacao()
        {
            try
            {
                var result = await _.ObterTodos();
                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = e.Message,
                    Excecao = "Erro ao realizar a consulta"
                });
            }
        }
    }
}