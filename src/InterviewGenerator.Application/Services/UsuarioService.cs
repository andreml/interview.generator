using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Repositorio;

namespace InterviewGenerator.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _repositorio;
        
        public UsuarioService(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ResponseBase> AlterarUsuario(AlterarUsuarioDto usuarioDto)
        {
            var response = new ResponseBase();

            var usuario = await _repositorio.ObterPorId(usuarioDto.Id);
            if(usuario == null)
            {
                response.AddErro("Usuário não encontrado");
                return response;
            }

            if (!usuarioDto.Cpf.Equals(usuario.Cpf))
            {
                var usuarioPorCpf = await _repositorio.ExisteUsuarioPorCpf(usuarioDto.Cpf);
                if (usuarioPorCpf)
                {
                    response.AddErro("Já existe um usuário com este CPF");
                    return response;
                }
            }

            if (!usuarioDto.Login.Equals(usuario.Login))
            {
                var usuarioPorLogin = await _repositorio.ExisteUsuarioPorLogin(usuarioDto.Login);
                if (usuarioPorLogin)
                {
                    response.AddErro("Já existe um usuário com este Login");
                    return response;
                }
            }

            usuario.Atualizar(usuarioDto.Cpf, usuarioDto.Nome, usuarioDto.Login, usuarioDto.Senha);

            await _repositorio.Alterar(usuario);

            response.AddData("Usuário alterado com sucesso!");
            return response;
        }

        public async Task<ResponseBase> CadastrarUsuario(AdicionarUsuarioDto usuario)
        {
            var response = new ResponseBase();

            var usuarioPorCpf = await _repositorio.ExisteUsuarioPorCpf(usuario.Cpf);
            if (usuarioPorCpf)
            {
                response.AddErro("Já existe um usuário com este CPF.");
                return response;
            }

            var usuarioPorLogin = await _repositorio.ExisteUsuarioPorLogin(usuario.Login);
            if (usuarioPorLogin)
            {
                response.AddErro("Já existe um usuário com este Login.");
                return response;
            }

            var novoUsuario = new Usuario(usuario.Cpf, usuario.Nome, usuario.Perfil, usuario.Login, usuario.Senha);

            await _repositorio.Adicionar(novoUsuario);

            response.AddData("Usuário adicionado com sucesso!");

            return response;
        }

        public async Task<ResponseBase<UsuarioViewModel>> ObterUsuario(Guid id)
        {
            var response = new ResponseBase<UsuarioViewModel>();

            var usuario = await _repositorio.ObterPorId(id);

            if (usuario == null)
                return response;

            response.AddData(new UsuarioViewModel(usuario.Id, usuario.Nome, usuario.Cpf, usuario.Login, usuario.Perfil));

            return response;
        }
    }
}
