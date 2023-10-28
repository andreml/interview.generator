using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Repositorio;
using interview.generator.domain.Utils;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace interview.generator.application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IConfiguration _configuration;

        public LoginService(IUsuarioRepositorio usuarioRepositorio, IConfiguration configuration)
        {
            _repositorio = usuarioRepositorio;
            _configuration = configuration;
        }
        public async Task<ResponseBase<LoginViewModel>> BuscarTokenUsuario(GerarTokenUsuarioDto usuario)
        {
            var response = new ResponseBase<LoginViewModel>();

            var user = await _repositorio.ObterUsuarioPorLoginESenha(usuario.Login, usuario.Senha);

            if (user != null)
            {
                var login = new LoginViewModel(user.Nome, user.Perfil, Jwt.GeraToken(user, user.VerificaValidadeTokenUsuario(), _configuration));
                response.AddData(login, HttpStatusCode.OK);
            }
            else
            {
                response.AddErro("Não foi possível gerar token para acesso do usuário");
            }


            return response;
        }
    }
}
