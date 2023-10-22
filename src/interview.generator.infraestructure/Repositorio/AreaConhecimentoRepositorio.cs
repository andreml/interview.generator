using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;
using interview.generator.infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace interview.generator.infraestructure.Repositorio
{
    public class AreaConhecimentoRepositorio : IAreaConhecimentoRepositorio
    {
        protected ApplicationDbContext _context;
        protected DbSet<AreaConhecimento> _dbSet;
        public AreaConhecimentoRepositorio(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<AreaConhecimento>();
        }

        public Task Adicionar(AreaConhecimento entity)
        {
            throw new NotImplementedException();
        }

        public Task Alterar(AreaConhecimento entity)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AreaConhecimento?> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<AreaConhecimento?> ObterPorIdEUsuarioId(Guid id, Guid usuarioId)
        {
            return await _context.AreaConhecimento.FirstOrDefaultAsync(x => x.Id == id && x.UsuarioId == usuarioId);
        }

        public Task<IEnumerable<AreaConhecimento>> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
