using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InterviewGenerator.Infra.Repositorio;

public class QuestionarioRepositorio : IQuestionarioRepositorio
{
    protected ApplicationDbContext _context;
    protected DbSet<Questionario> _dbSet;
    public QuestionarioRepositorio(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Questionario>();
    }
    public async Task Adicionar(Questionario entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Alterar(Questionario entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Excluir(Questionario entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Questionario?> ObterPorNome(Guid usuarioCriacaoId, string nome)
    {
        return await _dbSet
                        .Include(x => x.Perguntas)
                        .Where(x => x.UsuarioCriacaoId == usuarioCriacaoId
                                    && x.Nome == nome)
                        .FirstOrDefaultAsync();
    }

    public async Task<Questionario?> ObterPorId(Guid questionarioId)
    {
        return await _dbSet
                        .Include(x => x.Avaliacoes)
                            .ThenInclude(x => x.Candidato)
                        .Include(x => x.Perguntas)
                            .ThenInclude(x => x.Alternativas)
                        .Where(x => x.Id == questionarioId)
                        .FirstOrDefaultAsync();
    }

    public async Task<Questionario?> ObterPorIdComAvaliacoesEPerguntas(Guid usuarioCriacaoId, Guid id)
    {
        return await _dbSet
                        .Include(x => x.Perguntas)
                        .Include(x => x.Avaliacoes)
                            .ThenInclude(x => x.Candidato)    
                        .Where(x => x.UsuarioCriacaoId == usuarioCriacaoId
                                    && x.Id == id)
                        .FirstOrDefaultAsync();

    }

    public async Task<ICollection<Questionario>> ObterQuestionarios(Guid usuarioCriacaoId, Guid questionarioId, string? nome)
    {
        return await _dbSet
                        .Include(x => x.Avaliacoes)
                        .Include(x => x.Perguntas)
                        .Where(x => x.UsuarioCriacaoId == usuarioCriacaoId
                                    && (questionarioId == Guid.Empty || x.Id == questionarioId)
                                    && (string.IsNullOrEmpty(nome) || x.Nome.Contains(nome))
                        )
                        .OrderByDescending(x => x.DataCriacao)
                        .ToListAsync();
    }
}
