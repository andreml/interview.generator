using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Repositorio;

namespace interview.generator.application.Services
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

            usuario.Atualizar(usuarioDto.Cpf, usuarioDto.Nome, usuarioDto.Perfil, usuarioDto.Login, usuarioDto.Senha);

            await _repositorio.Alterar(usuario);

            response.AddData("Usuário alterado com sucesso!");
            return response;
        }

        public async Task<ResponseBase> CadastrarUsuario(AddUsuarioDto usuario)
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

        public async Task<ResponseBase> ExcluirUsuario(Guid id)
        {
            var response = new ResponseBase();

            //Adicionar validações

            await _repositorio.Excluir(id);

            response.AddData("Usuário excluído com sucesso!");

            return response;
        }

        public async Task<ResponseBase<IEnumerable<Usuario>>> ListarUsuarios()
        {
            var response = new ResponseBase<IEnumerable<Usuario>>();

            var usuarios = await _repositorio.ObterTodos();

            response.AddData(usuarios);

            return response;
        }

        public async Task<ResponseBase<Usuario>> ObterUsuario(Guid id)
        {
            var response = new ResponseBase<Usuario>();

            var usuario = await _repositorio.ObterPorId(id);

            response.AddData(usuario!);

            return response;
        }
    }
}
