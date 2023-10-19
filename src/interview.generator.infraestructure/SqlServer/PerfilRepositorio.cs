using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;

namespace interview.generator.infraestructure.SqlServer
{
    public class PerfilRepositorio : IPerfilRepositorio
    {
        List<Perfil> perfil;
        public PerfilRepositorio()
        {
            this.perfil = new List<Perfil>();
        }
        public Task Adicionar(Perfil entity)
        {
            perfil.Add(entity);
            return Task.CompletedTask;
        }

        public Task Alterar(Perfil entity)
        {
            if (perfil.Count > 0)
            {
                foreach (var p in perfil)
                {
                    if (p.Id == entity.Id)
                    {
                        perfil.Remove(p);
                        perfil.Add(entity);
                        break;
                    }
                }
            }

            return Task.CompletedTask;
        }

        public Task Excluir(Guid id)
        {
            if (perfil.Count > 0)
            {
                var result = perfil.Where(x => x.Id == id).FirstOrDefault();
                if (result != null) perfil.Remove(result);
            }

            return Task.CompletedTask;
        }

        public async Task<Perfil> ObterPorId(Guid id)
        {
            return await Task.FromResult(perfil.FirstOrDefault(x => x.Id.Equals(id)));
        }

        public async Task<IEnumerable<Perfil>> ObterTodos()
        {
            return await Task.FromResult(perfil);
        }
    }
}
