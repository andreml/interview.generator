using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;
using Microsoft.AspNetCore.Mvc;

namespace interview.generator.api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento do Login
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {

        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
            
        }

        /// <summary>
        /// Gera token do usuário (Avaliador | Candidato)
        /// </summary>
        [HttpPost("GerarToken")]
        [ProducesResponseType(typeof(LoginViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GerarToken(GerarTokenUsuarioDto usuario)
        {
            try
            {
                var result = await _loginService.BuscarTokenUsuario(usuario);

                return Response(result!);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao gerar token");
            }
        }
    }
}
