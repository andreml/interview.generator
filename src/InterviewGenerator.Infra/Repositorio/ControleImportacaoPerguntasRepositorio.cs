using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InterviewGenerator.Infra.Repositorio
{
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

        public async Task<ControleImportacaoPerguntas?> ObterControleImportacaoPorIdArquivo(Guid arquivoId)
        {
            return await _dbSet
                            .Where(x => x.Id == arquivoId)
                            .Include(x => x.LinhasArquivo)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ControleImportacaoPerguntas>> ObterControlesImportacao(Guid usuarioId)
        {
            return await _dbSet
                            .Where(x => x.UsuarioId == usuarioId)
                            .Include(x => x.LinhasArquivo)
                            .ToListAsync();
        }
    }
}
