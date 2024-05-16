using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InterviewGenerator.Infra.Repositorio;

public class AreaConhecimentoRepositorio : IAreaConhecimentoRepositorio
{
    protected ApplicationDbContext _context;
    protected DbSet<AreaConhecimento> _dbSet;
    public AreaConhecimentoRepositorio(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<AreaConhecimento>();
    }

    public async Task Adicionar(AreaConhecimento entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Alterar(AreaConhecimento entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Excluir(AreaConhecimento entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
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

    public async Task<int> ObterCountAsync(Guid usuarioCriacaoId) =>
        await _dbSet
                .Where(a => a.UsuarioCriacaoId == usuarioCriacaoId)
                .CountAsync();
}
