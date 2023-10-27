using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;
using interview.generator.infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace interview.generator.infraestructure.Repositorio
{
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
            await _context.Questionario.AddAsync(entity);
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
            return await _context.Questionario
                        .Include(x => x.PerguntasQuestionario)
                        .Where(x => x.UsuarioCriacaoId == usuarioCriacaoId
                                    && x.Nome == nome)
                        .FirstOrDefaultAsync();              
        }

        public async Task<Questionario?> ObterPorId(Guid questionarioId)
        {
            return await _context.Questionario
                        .Include(x => x.Avaliacoes)
                        .Include(x => x.PerguntasQuestionario)
                            .ThenInclude(x => x.Pergunta)
                                .ThenInclude(x => x.Alternativas)
                        .Where(x => x.Id == questionarioId)
                        .FirstOrDefaultAsync();
        }

        public async Task<Questionario?> ObterPorIdComAvaliacoesEPerguntas(Guid usuarioCriacaoId, Guid id)
        {
            return await _context.Questionario
                        .Include(x => x.PerguntasQuestionario)
                        .Include(x => x.Avaliacoes)
                        .Where(x => x.UsuarioCriacaoId == usuarioCriacaoId
                                    && x.Id == id)
                        .FirstOrDefaultAsync();
            
        }

        public async Task<ICollection<Questionario>> ObterQuestionarios(Guid usuarioCriacaoId, Guid questionarioId, string? nome)
        {
            return await _context.Questionario
                                    .Include(x => x.PerguntasQuestionario)
                                        .ThenInclude(x => x.Pergunta)
                                    .Where(x => x.UsuarioCriacaoId == usuarioCriacaoId
                                                && (questionarioId == Guid.Empty || x.Id == questionarioId)
                                                && (string.IsNullOrEmpty(nome) || x.Nome.Contains(nome))
                                    )
                                    .OrderByDescending(x => x.DataCriacao)
                                    .ToListAsync();
        }
    }
}
