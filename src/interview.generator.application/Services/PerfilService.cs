using interview.generator.application.Interfaces;
using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;

namespace interview.generator.application.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepositorio _repositorio;
        public PerfilService(IPerfilRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task AlterarPerfil(Perfil Perfil)
        {
            await _repositorio.Alterar(Perfil);
        }

        public async Task CadastrarPerfil(Perfil Perfil)
        {
            await _repositorio.Adicionar(Perfil);
        }

        public async Task ExcluirPerfil(int id)
        {
            await _repositorio.Excluir(id);
        }

        public async Task<IEnumerable<Perfil>> ListarPerfils()
        {
            var Perfils = _repositorio.ObterTodos().Result;
            return await Task.FromResult(Perfils);
        }

        public async Task<Perfil> ObterPerfil(int id)
        {
            var Perfil = _repositorio.ObterPorId(id).Result;
            return await Task.FromResult(Perfil);
        }
    }
}
