using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InterviewGenerator.Infra.Repositorio;

public class LinhasArquivoRepositorio : ILinhasArquivoRepositorio
{
    protected ApplicationDbContext _context;
    protected DbSet<LinhaArquivo> _dbSet;

    public LinhasArquivoRepositorio(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<LinhaArquivo>();
    }
    public async Task Adicionar(LinhaArquivo entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Alterar(LinhaArquivo entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<LinhaArquivo?> ObterLinhaArquivo(Guid idControleImportacao, int idLinha)
    {
        return await _dbSet
                        .FirstOrDefaultAsync(x => x.IdControleImportacao == idControleImportacao && x.NumeroLinha == idLinha);
    }
}
