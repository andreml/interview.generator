using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;
using interview.generator.infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace interview.generator.infraestructure.Repositorio
{
    public class RespostaAvaliacaoRepositorio : IRespostaAvaliacaoRepositorio
    {
        protected ApplicationDbContext _context;
        protected DbSet<RespostaAvaliacao> _dbSet;
        public RespostaAvaliacaoRepositorio(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<RespostaAvaliacao>();
        }
        public async Task Adicionar(RespostaAvaliacao entity)
        {
            await _context.RespostaAvaliacao.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Alterar(RespostaAvaliacao entity)
        {
            var respostaAvaliacao = await _context.RespostaAvaliacao.FindAsync(entity);
            if (respostaAvaliacao is null) throw new Exception("Não foi possível alterar, Resposta da Avaliação não existe mais");
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public Task Excluir(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<RespostaAvaliacao?> ObterRespostaPorPergunta(Guid PerguntaId)
            => await _context.RespostaAvaliacao.Where(x => x.PerguntaQuestionarioId == PerguntaId).FirstOrDefaultAsync();
        public async Task<RespostaAvaliacao?> ObterPorId(Guid id)
            => await _context.RespostaAvaliacao.Where(x => x.AvaliacaoId.Equals(id)).FirstOrDefaultAsync();
        public async Task<IEnumerable<RespostaAvaliacao>> ObterTodos()
            => await _context.RespostaAvaliacao.ToListAsync();
    }
}
