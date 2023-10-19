using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;

namespace interview.generator.infraestructure.SqlServer
{
    public class TipoQuestionarioRepositorio : ITipoQuestionarioRepositorio
    {
        List<TipoQuestionario> tipoQuestionario;
        public TipoQuestionarioRepositorio()
        {
            this.tipoQuestionario = new List<TipoQuestionario>();
        }
        public Task Adicionar(TipoQuestionario entity)
        {
            tipoQuestionario.Add(entity);
            return Task.CompletedTask;
        }

        public Task Alterar(TipoQuestionario entity)
        {
            if (tipoQuestionario.Count > 0)
            {
                foreach (var p in tipoQuestionario)
                {
                    if (p.Id == entity.Id)
                    {
                        tipoQuestionario.Remove(p);
                        tipoQuestionario.Add(entity);
                        break;
                    }
                }
            }

            return Task.CompletedTask;
        }

        public Task Excluir(Guid id)
        {
            if (tipoQuestionario.Count > 0)
            {
                var result = tipoQuestionario.Where(x => x.Id == id).FirstOrDefault();
                if (result != null) tipoQuestionario.Remove(result);
            }

            return Task.CompletedTask;
        }

        public async Task<TipoQuestionario> ObterPorId(Guid id)
        {
            return await Task.FromResult(tipoQuestionario.FirstOrDefault(x => x.Id.Equals(id)));
        }

        public async Task<IEnumerable<TipoQuestionario>> ObterTodos()
        {
            return await Task.FromResult(tipoQuestionario);
        }
    }
}
