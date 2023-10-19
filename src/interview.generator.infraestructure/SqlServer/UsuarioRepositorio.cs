using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;

namespace interview.generator.infraestructure.SqlServer
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Adicionar(Usuario entity)
        {
            _context.Usuario.Add(entity);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Alterar(Usuario entity)
        {
            throw new NotImplementedException();
            //if (usuarios.Count > 0)
            //{
            //    foreach (var usuario in usuarios)
            //    {
            //        if (usuario.Id == entity.Id)
            //        {
            //            usuarios.Remove(usuario);
            //            usuarios.Add(entity);
            //            break;
            //        }
            //    }
            //}

            //return Task.CompletedTask;
        }

        public Task Excluir(Guid id)
        {
            //if (usuarios.Count > 0)
            //{
            //    var result = usuarios.Where(x => x.Id == id).FirstOrDefault();
            //    if (result != null) usuarios.Remove(result);
            //}

            return Task.CompletedTask;
        }

        public async Task<Usuario> ObterPorId(Guid id)
        {
            //return await Task.FromResult(usuarios.FirstOrDefault(x => x.Id.Equals(id)));
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
