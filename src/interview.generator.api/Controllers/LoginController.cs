using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.domain.Entidade.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace interview.generator.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {

        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
            
        }

        [HttpPost("GeraToken")]
        public async Task<IActionResult> GeraToken(GeraTokenUsuario usuario)
        {
            try
            {
                var result = await _loginService.BuscaTokenUsuario(usuario);

                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = e.Message,
                    Excecao = ""
                });
            }
        }
    }
}
