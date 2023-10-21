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

            var usuarioPorCpf = await _repositorio.ObterUsuarioPorCpf(usuarioDto.Cpf);
            if(usuarioPorCpf != null && usuarioPorCpf.Id != usuario.Id)
            {
                response.AddErro("Já existe um usuário com este CPF");
                return response;
            }

            usuario.Atualizar(usuarioDto.Cpf, usuarioDto.Nome, usuarioDto.Perfil);

            await _repositorio.Alterar(usuario);

            response.AddData("Usuário alterado com sucesso!");
            return response;
        }

        public async Task<ResponseBase> CadastrarUsuario(AddUsuarioDto usuario)
        {
            var response = new ResponseBase();

            var usuarioPorCpf = _repositorio.ObterUsuarioPorCpf(usuario.Cpf);

            if(usuarioPorCpf != null)
            {
                response.AddErro("Já existe um usuário com este CPF.");
                return response;
            }

            var novoUsuario = new Usuario()
            {
                Nome = usuario.Nome,
                Perfil = usuario.Perfil,
                Cpf = usuario.Cpf,
            };

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
