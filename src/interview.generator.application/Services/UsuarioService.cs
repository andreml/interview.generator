using interview.generator.application.DTO;
using interview.generator.application.Interfaces;
using interview.generator.domain.Entidade;
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

        public async Task AlterarUsuario(Usuario usuario)
        {
            await _repositorio.Alterar(usuario);
        }

        public async Task CadastrarUsuario(AdicionarUsuarioDTO usuarioDto)
        {
            var usuario = new Usuario()
            {
                Cpf = usuarioDto.Cpf,
                Nome = usuarioDto.Nome,
                Perfil = usuarioDto.Perfil
            };

            await _repositorio.Adicionar(usuario);
        }

        public async Task ExcluirUsuario(Guid id)
        {
            await _repositorio.Excluir(id);
        }

        public async Task<IEnumerable<Usuario>> ListarUsuarios()
        {
            var usuarios = _repositorio.ObterTodos().Result;
            return await Task.FromResult(usuarios);
        }

        public async Task<Usuario> ObterUsuario(Guid id)
        {
            var usuario = _repositorio.ObterPorId(id).Result;
            return await Task.FromResult(usuario);
        }
    }
}
