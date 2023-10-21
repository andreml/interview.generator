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
            _context.Usuario.Add(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Alterar(Usuario entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Excluir(Guid id)
        {
            var result = ObterPorId(id).Result;
            if (result is null) throw new Exception("Usuario não encontrado");

            _dbSet.Remove(result);
            return Task.CompletedTask;
        }

        public async Task<Usuario> ObterPorId(Guid id)
        {
            var result = _dbSet.FirstOrDefaultAsync(u => u.Id.Equals(id)).Result;
            if (result is null) throw new Exception("Usuario não encontrado");
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Usuario>> ObterTodos()
        {
            var result = _dbSet.ToListAsync().Result;
            return await Task.FromResult(result);
        }

        public Task<Usuario?> ObterUsuarioPorLoginESenha(string login, string senha)
        {
            return  _dbSet.FirstOrDefaultAsync(u => u.Login == login && u.Senha == Encryptor.Encrypt(senha));
        }

        public async Task<Usuario?> ObterUsuarioPorCpf(string cpf)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Cpf == cpf);
        }
    }
}
