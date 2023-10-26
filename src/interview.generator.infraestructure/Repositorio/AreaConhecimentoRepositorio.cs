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
            _context.AreaConhecimento.Add(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Alterar(AreaConhecimento entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Excluir(AreaConhecimento entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<AreaConhecimento?> ObterPorIdComPerguntas(Guid usuarioCriacaoId, Guid id)
        {
            return await _dbSet
                            .Include(x => x.Perguntas)
                            .FirstOrDefaultAsync(u => u.Id.Equals(id)
                                                 && u.UsuarioCriacaoId == usuarioCriacaoId);
        }

        public async Task<AreaConhecimento?> ObterPorIdEUsuarioId(Guid usuarioCriacaoId, Guid id)
        {
            return await _dbSet
                            .FirstOrDefaultAsync(u => u.Id.Equals(id)
                                                 && u.UsuarioCriacaoId == usuarioCriacaoId);
        }

        public async Task<AreaConhecimento?> ObterPorDescricaoEUsuarioId(Guid usuarioCriacaoId, string descricao)
        {
            return await _context.AreaConhecimento.FirstOrDefaultAsync(x => x.Descricao == descricao && x.UsuarioCriacaoId == usuarioCriacaoId);
        }

        public async Task<IEnumerable<AreaConhecimento>> ObterAreaConhecimentoComPerguntas(Guid usuarioCriacaoId, Guid areaConhecimentoId, string? descricao)
        {
            return await _dbSet
                            .Include(x => x.Perguntas)
                            .Where(x => x.UsuarioCriacaoId == usuarioCriacaoId
                                    && (areaConhecimentoId == Guid.Empty || areaConhecimentoId == x.Id)
                                    && (string.IsNullOrEmpty(descricao) || x.Descricao.Contains(descricao)))
                            .ToListAsync();
        }
    }
}
