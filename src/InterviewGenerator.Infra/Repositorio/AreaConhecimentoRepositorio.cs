using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InterviewGenerator.Infra.Repositorio
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
            _dbSet.Add(entity);
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
            return await _dbSet.FirstOrDefaultAsync(x => x.Descricao == descricao && x.UsuarioCriacaoId == usuarioCriacaoId);
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
