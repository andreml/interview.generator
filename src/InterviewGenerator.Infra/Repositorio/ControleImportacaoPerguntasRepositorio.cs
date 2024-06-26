﻿using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InterviewGenerator.Infra.Repositorio;

public class ControleImportacaoRepositorio : IControleImportacaoPerguntasRepositorio
{
    protected ApplicationDbContext _context;
    protected DbSet<ControleImportacaoPerguntas> _dbSet;

    public ControleImportacaoRepositorio(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<ControleImportacaoPerguntas>();
    }

    public async Task Adicionar(ControleImportacaoPerguntas entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Alterar(ControleImportacaoPerguntas entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ControleImportacaoPerguntas>> ObterControlesImportacao(Guid usuarioId)
    {
        return await _dbSet
                        .Include(x => x.LinhasArquivo)
                        .Where(x => x.UsuarioId == usuarioId)
                        .OrderByDescending(x => x.DataUpload)
                        .ToListAsync();
    }
}
