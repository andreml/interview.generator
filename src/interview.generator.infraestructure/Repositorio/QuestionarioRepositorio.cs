using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;
using interview.generator.infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task Alterar(Questionario entity)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(Questionario entity)
        {
            throw new NotImplementedException();
        }

        public Task<Questionario?> ObterPorNome(string nome)
        {
            return _context.Questionario
                        .Where(x => x.Nome == nome).FirstOrDefaultAsync();
                        ;
        }

        public Task<Questionario?> ObterPorId(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
