using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
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
        public async Task<ResponseBase<object>> BuscaTokenUsuario(GeraTokenUsuario usuario)
        {
            var response = new ResponseBase<object>();

            var user = await _repositorio.ObterUsuarioPorLoginESenha(usuario.Login, usuario.Senha);

            if (user != null)
            {
                response.AddData(new { Usuario = new { Nome = user.Nome, Perfil = user.Perfil.ToString() }, Token = Jwt.GeraToken(user, user.VerificaValidadeTokenUsuario(), _configuration) });
                response.SetStatusCode(HttpStatusCode.OK);
            }
            else
            {
                response.AddErro("Não foi possível gerar token para acesso do usuário");
            }


            return response;
        }
    }
}
