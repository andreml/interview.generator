using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewGenerator.Infra.Repositorio
{
    public class LinhasArquivoRepositorio : ILinhasArquivoRepositorio
    {
        protected ApplicationDbContext _context;
        protected DbSet<LinhasArquivo> _dbSet;

        public LinhasArquivoRepositorio(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<LinhasArquivo>();
        }
        public async Task Adicionar(LinhasArquivo entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

        }

        public Task Alterar(LinhasArquivo entity)
        {
            throw new NotImplementedException();
        }
    }
}
