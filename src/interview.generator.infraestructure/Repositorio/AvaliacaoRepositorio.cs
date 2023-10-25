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

        public async Task<Avaliacao?> ObterAvaliacaoPorFiltro(Guid usuarioIdCriacaoQuestionario, Guid? candidatoId, Guid? questionarioId)
        {
            var resultado = await _context.Avaliacao.Include(x => x.Respostas)
                                                    .Where(x => x.Questionario.UsuarioCriacaoId == usuarioIdCriacaoQuestionario &&
                                                                ( x.CandidatoId.Equals(candidatoId!) ||
                                                                  x.Questionario.Id.Equals(questionarioId!))
                                                    )
                                                    .FirstOrDefaultAsync();

            return resultado;
        }

        public async Task<Avaliacao?> ObterAvaliacaoPorIdEUsuarioCriacaoQuestionario(Guid id, Guid usuarioIdCriacaoQuestionario)
        {
            return await _dbSet
                        .Where(x => x.Id == id
                               && x.Questionario.UsuarioCriacaoId == usuarioIdCriacaoQuestionario)
                        .FirstOrDefaultAsync();
        }


        public async Task Alterar(Avaliacao entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
