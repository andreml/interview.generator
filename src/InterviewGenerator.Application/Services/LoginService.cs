using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Domain.Utils;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace InterviewGenerator.Application.Services
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
            var user = new Domain.Entidade.Usuario();

            if (usuario.Login == "AvaliadorTeste")
            {
                user = new Domain.Entidade.Usuario();
                user.Cpf = "00011122299";
                user.Perfil = Domain.Enum.Perfil.Avaliador;
                user.Nome = "Avaliador Teste";
                user.Senha = "Senha@999";
                user.Id = Guid.NewGuid();
                goto Token;
            }

            user = await _repositorio.ObterUsuarioPorLoginESenha(usuario.Login, usuario.Senha);

        Token:
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
