using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InterviewGenerator.Infra.Repositorio
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
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Avaliacao>> ObterAvaliacoesPorFiltro(Guid usuarioIdCriacaoQuestionario, Guid QuestionarioId, string? nomeQuestionario, string? nomeCandidato)
        {
            return await _dbSet
                            .Include(x => x.Respostas)
                                .ThenInclude(x => x.AlternativaEscolhida)
                            .Include(x => x.Respostas)
                                .ThenInclude(x => x.Pergunta)
                            .Include(x => x.Candidato)
                            .Include(x => x.Questionario)
                            .Where(x => x.Questionario.UsuarioCriacaoId == usuarioIdCriacaoQuestionario
                                        && (QuestionarioId == Guid.Empty || x.Questionario.Id == QuestionarioId)
                                        && (string.IsNullOrEmpty(nomeQuestionario) || x.Questionario.Nome.Contains(nomeQuestionario))
                                        && (string.IsNullOrEmpty(nomeCandidato) || x.Candidato.Nome.Contains(nomeCandidato))
                                   )
                                   .ToListAsync();
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
