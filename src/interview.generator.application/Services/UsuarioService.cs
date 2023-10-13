using interview.generator.application.Interfaces;
using interview.generator.domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview.generator.application.Services
{
    public class UsuarioService : IUsuarioService
    {
        Task<IEnumerable<Usuario>> IUsuarioService.ListarUsuarios()
        {
            var usuarios = new List<Usuario>();
            usuarios.Add(new Usuario()
            {
                Cpf = "99999999",
                Id = 1,
                Nome = "Andre",
                PerfilId = "1"
            });

            return Task.FromResult(usuarios.AsEnumerable());
        }

        Task<Usuario> IUsuarioService.ObterUsuario(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
