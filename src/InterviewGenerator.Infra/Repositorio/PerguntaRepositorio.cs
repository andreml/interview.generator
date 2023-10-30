using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InterviewGenerator.Infra.Repositorio
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
        public async Task<bool> ExistePorDescricao(Guid usuarioCriacaoId, string descricao)
        {
            return await _dbSet.AnyAsync(x => x.UsuarioCriacaoId == usuarioCriacaoId
                                              && x.Descricao == descricao);
        }

        public async Task<Pergunta?> ObterPerguntaPorId(Guid usuarioCriacaoId, Guid perguntaId)
        {
            return await _dbSet
                            .Include(x => x.Questionarios)
                            .FirstOrDefaultAsync(x => x.UsuarioCriacaoId == usuarioCriacaoId
                                                 && x.Id == perguntaId);
        }

        public IEnumerable<Pergunta> ObterPerguntas(Guid usuarioCriacaoId, Guid perguntaId, string? areaConhecimento, string? descricao)
        {
            return _dbSet
                        .Include(x => x.AreaConhecimento)
                        .Include(x => x.Alternativas)
                        .Where(x =>
                            x.UsuarioCriacaoId == usuarioCriacaoId
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
