using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;
using interview.generator.infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace interview.generator.infraestructure.Repositorio
{
    public class PerguntaRepositorio : IPerguntaRepositorio
    {
        protected ApplicationDbContext _context;
        protected DbSet<Pergunta> _dbSet;
        public PerguntaRepositorio(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Pergunta>();
        }

        public async Task Adicionar(Pergunta entity)
        {
            await _context.Pergunta.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistePorDescricao(string descricao, Guid usuarioId)
        {
            return await _context.Pergunta.AnyAsync(x => x.UsuarioCriacaoId == usuarioId 
                                                         && x.Descricao == descricao);
        }

        public async Task<Pergunta?> ObterPerguntaPorId(Guid usuarioId, Guid perguntaId)
        {
            return await _context.Pergunta
                            .FirstOrDefaultAsync(x => x.UsuarioCriacaoId == usuarioId
                                                 && x.Id == perguntaId);
        }

        public IEnumerable<Pergunta> ObterPerguntas(Guid usuarioId, Guid perguntaId, string? areaConhecimento, string? descricao)
        {
            return _context.Pergunta
                        .Include(x => x.AreaConhecimento)
                        .Include(x => x.Alternativas)
                        .Where(x =>
                            x.UsuarioCriacaoId == usuarioId
                            && (perguntaId == Guid.Empty || x.Id == perguntaId)
                            && (areaConhecimento == null || x.AreaConhecimento.Descricao.Contains(areaConhecimento!))
                            && (descricao == null || x.Descricao.Contains(descricao!))
                        );
        }

        public async Task Alterar(Pergunta entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Pergunta entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
