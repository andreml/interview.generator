using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Domain.Utils;
using InterviewGenerator.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InterviewGenerator.Infra.Repositorio;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    protected ApplicationDbContext _context;
    protected DbSet<Usuario> _dbSet;
    public UsuarioRepositorio(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Usuario>();
    }

    public async Task Adicionar(Usuario entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Alterar(Usuario entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
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
