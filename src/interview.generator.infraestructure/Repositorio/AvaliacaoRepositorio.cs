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
        public async Task<Avaliacao?> ObterAvaliacaoPorFiltro(Guid? CandidatoId, Guid? QuestionarioId)
        {
            var resultado = await _context.Avaliacao.Include(x => x.Respostas)
                                                    .Where(x => x.CandidatoId.Equals(CandidatoId!) ||
                                                                x.QuestionarioId.Equals(QuestionarioId!))
                                                    .FirstOrDefaultAsync();

            return resultado;
        }

        public async Task AdicionarObservacaoAvaliacao(Guid? CandidatoId, Guid? QuestionarioId, string Observacao)
        {
            var avaliacao = await _context.Avaliacao.Include(x => x.Respostas)
                                                    .Where(x => x.QuestionarioId.Equals(QuestionarioId) &&
                                                                x.CandidatoId.Equals(CandidatoId))
                                                    .FirstOrDefaultAsync();

            var existeAlternativaNaoRespondida = false;

            if (avaliacao != null && avaliacao.Respostas != null)
            {
                foreach (var resposta in avaliacao.Respostas)
                {
                    if (resposta.AlternativaEscolhidaId == Guid.Empty)
                    {
                        existeAlternativaNaoRespondida = true;
                        break;
                    }
                }
            }

            if (existeAlternativaNaoRespondida) throw new ApplicationException("Avaliação em aberto porque existem perguntas não respondidas");

            if (avaliacao != null)
            {
                avaliacao.ObservacaoAplicador = Observacao;
                _context.Avaliacao.Update(avaliacao);
                await _context.SaveChangesAsync();
            }
        }
    }
}
