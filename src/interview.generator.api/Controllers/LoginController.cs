using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
                return ResponseErro(e.Message, "Erro ao gerar token");
            }
        }
    }
}
