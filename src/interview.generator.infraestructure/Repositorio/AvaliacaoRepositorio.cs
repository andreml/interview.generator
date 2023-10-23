using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;
using interview.generator.infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace interview.generator.infraestructure.Repositorio
{
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
            await _context.Avaliacao.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Alterar(Avaliacao entity)
        {
            var Avaliacao = await _context.Avaliacao.Where(x => x.Id.Equals(entity.Id)).FirstOrDefaultAsync();
            if (Avaliacao is null) throw new Exception("Não foi possível alterar, Avaliação não existe mais");
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Avaliacao?> ObterAvaliacaoPorFiltro(Guid? CandidatoId, Guid? QuestionarioId, DateTime? DataAplicacao)
            => await _context.Avaliacao.Where(x => x.CandidatoId.Equals(CandidatoId!) ||
                                                   x.QuestionarioId.Equals(QuestionarioId!) ||
                                                   x.DataAplicacao.Equals(DataAplicacao!))
                                       .FirstOrDefaultAsync();
        public async Task<IEnumerable<Avaliacao>> ObterTodos()
            => await _context.Avaliacao.ToListAsync();
    }
}
