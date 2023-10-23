﻿using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;
using interview.generator.infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace interview.generator.infraestructure.Repositorio
{
    public class AreaConhecimentoRepositorio : IAreaConhecimentoRepositorio
    {
        protected ApplicationDbContext _context;
        protected DbSet<AreaConhecimento> _dbSet;
        public AreaConhecimentoRepositorio(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<AreaConhecimento>();
        }

        public Task Adicionar(AreaConhecimento entity)
        {
            _context.AreaConhecimento.Add(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Alterar(AreaConhecimento entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Excluir(Guid id)
        {
            var result = ObterPorId(id).Result;
            if (result is null) throw new Exception("Area de conhecimento não encontrada");

            _context.Remove(result);
            return Task.CompletedTask;
        }

        public async Task<AreaConhecimento?> ObterPorDescricao(string descricao)
        {
            return await _context.AreaConhecimento
                                .Include(x => x.Perguntas)
                                .FirstOrDefaultAsync(x => x.Descricao == descricao);
        }

        public async Task<AreaConhecimento?> ObterPorId(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<AreaConhecimento?> ObterPorIdComPerguntas(Guid id)
        {
            return await _dbSet
                            .Include(x => x.Perguntas)
                            .FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<AreaConhecimento?> ObterPorIdEUsuarioId(Guid id, Guid usuarioId)
        {
            return await _context.AreaConhecimento.FirstOrDefaultAsync(x => x.Id == id && x.UsuarioId == usuarioId);
        }

        public async Task<AreaConhecimento?> ObterPorDescricaoEUsuarioId(string descricao, Guid usuarioId)
        {
            return await _context.AreaConhecimento.FirstOrDefaultAsync(x => x.Descricao == descricao && x.UsuarioId == usuarioId);
        }

        public async Task<IEnumerable<AreaConhecimento>> ObterTodos()
        {
            var result = _dbSet.ToListAsync().Result;
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<AreaConhecimento>> ObterTodosPorUsuarioIdComPerguntas(Guid usuarioId)
        {
            return await _dbSet
                            .Include(x => x.Perguntas)
                            .Where(x => x.UsuarioId == usuarioId)
                            .ToListAsync();
        }
    }
}
