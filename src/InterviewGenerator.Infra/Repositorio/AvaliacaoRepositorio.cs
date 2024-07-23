using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InterviewGenerator.Infra.Repositorio;

public class AvaliacaoRepositorio : IAvaliacaoRepositorio
{
    protected ApplicationDbContext _context;
    protected DbSet<Avaliacao> _dbSet;

    public AvaliacaoRepositorio(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Avaliacao>();
    }

    public async Task Adicionar(Avaliacao entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Avaliacao?> ObterPorIdEUsuarioCriacaoQuestionarioAsync(Guid usuarioIdCriacaoQuestionario, Guid avaliacaoId)
    {
        return await _dbSet!
                        .Include(x => x.Respostas)!
                            .ThenInclude(x => x.AlternativaEscolhida)!
                        .Include(x => x.Respostas)!
                            .ThenInclude(x => x.Pergunta)
                        .Include(x => x.Candidato)
                        .Include(x => x.Questionario)
                        .Where(x => x.Questionario.UsuarioCriacaoId == usuarioIdCriacaoQuestionario && x.Id == avaliacaoId)
                        .FirstOrDefaultAsync();
    }

    public async Task<Avaliacao?> ObterPorIdEUsuarioCriacaoQuestionario(Guid id, Guid usuarioIdCriacaoQuestionario)
    {
        return await _dbSet
                    .Where(x => x.Id == id
                           && x.Questionario.UsuarioCriacaoId == usuarioIdCriacaoQuestionario)
                    .FirstOrDefaultAsync();
    }

    public async Task<Avaliacao?> ObterPorIdECandidatoId(Guid id, Guid candidatoId)
    {
        return await _dbSet
                    .Include(x => x.Questionario)
                        .ThenInclude(x => x.Perguntas)
                            .ThenInclude(x => x.Alternativas)
                    .Where(x => x.Id == id
                           && x.Candidato.Id == candidatoId)
                    .FirstOrDefaultAsync();
    }

    public async Task<ICollection<Avaliacao>> ObterPorCandidatoId(Guid candidatoId)
    {
        return await _dbSet
                    .Include(x => x.Questionario)
                    .Where(x => x.Candidato.Id == candidatoId)
                    .ToListAsync();
    }

    public async Task<ICollection<Avaliacao>> ObterPorUsuarioCriacaoEQuestionarioId(Guid usuarioCriacaoId, Guid questionarioId)
    {
        return await _dbSet
                    .Include(x => x.Questionario)
                    .Where(x => x.Questionario.UsuarioCriacaoId == usuarioCriacaoId 
                            && x.Questionario.Id == questionarioId)
                    .ToListAsync();
    }

    public async Task Alterar(Avaliacao entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}
