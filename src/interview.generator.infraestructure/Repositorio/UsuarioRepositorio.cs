using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;
using interview.generator.domain.Utils;
using interview.generator.infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace interview.generator.infraestructure.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        protected ApplicationDbContext _context;
        protected DbSet<Usuario> _dbSet;
        public UsuarioRepositorio(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Usuario>();
        }

        public Task Adicionar(Usuario entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Alterar(Usuario entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<Usuario?> ObterPorId(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public Task<Usuario?> ObterUsuarioPorLoginESenha(string login, string senha)
        {
            return  _dbSet.FirstOrDefaultAsync(u => u.Login == login && u.Senha == Encryptor.Encrypt(senha));
        }

        public async Task<bool> ExisteUsuarioPorCpf(string cpf)
        {
            return await _dbSet.AnyAsync(u => u.Cpf == cpf);
        }

        public async Task<bool> ExisteUsuarioPorLogin(string login)
        {
            return await _dbSet.AnyAsync(u => u.Login == login);
        }
    }
}
