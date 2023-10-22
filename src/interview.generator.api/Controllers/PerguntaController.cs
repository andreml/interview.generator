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
    public class PerguntaController : BaseController
    {
        readonly IPerguntaService _perguntaService;
        public PerguntaController(IPerguntaService perguntaService)
        {
            _perguntaService = perguntaService;
        }

        [HttpGet("ObterTodos")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public IActionResult ObterUsuariosAsync()
        {
            try
            {
                var userId = ObterUsuarioIdLogado();

                var result = _perguntaService.ListarPerguntasPorUsuario(userId);

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

        [HttpPost("AdicionarPergunta")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> AdicionarUsuario(AdicionarPerguntaDto pergunta)
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