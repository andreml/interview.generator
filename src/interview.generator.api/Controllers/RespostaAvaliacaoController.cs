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
    public class RespostaAvaliacaoController : BaseController
    {
        readonly IRespostaAvaliacaoService _;
        public RespostaAvaliacaoController(IRespostaAvaliacaoService service) { _ = service; }

        [HttpGet("ObterRespostasPorAvaliacao/{avaliacaoId}")]
        //[Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> ObterRespostaPorAvalicacaoAsync(Guid avaliacaoId)
        {
            try
            {
                var result = await _.ObterRespostasPorAvaliacao(avaliacaoId);
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

        [HttpGet("ObterRespostasPorPergunta/{perguntaId}")]
        //[Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> ObterRespostaPorPerguntaAsync(Guid perguntaId)
        {
            try
            {
                var result = await _.ObterRespostaPorPergunta(perguntaId);
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

        [HttpPost("AdicionarRespostaAvaliacao")]
        //[Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> AdicionarRespostaAvaliacao(AdicionarRespostaAvaliacaoDto obj)
        {
            try
            {
                var result = await _.AdicionarRespostaAvaliacao(obj);
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

        [HttpPut("AlterarRespostaAvaliacao")]
        ///[Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> AlterarRespostaAvaliacao(AlterarRespostaAvaliacaoDto obj)
        {
            try
            {
                //if (ObterUsuarioIdLogado() != usuario.Id)
                //    return Unauthorized();

                var result = await _.AlterarRespostaAvaliacao(obj);

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

        [HttpGet("ObterTodasRespostas")]
        //[Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> ObterTodasRespostaAvaliacao()
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