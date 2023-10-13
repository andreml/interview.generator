using interview.generator.application.Interfaces;
using interview.generator.application.Services;
using interview.generator.domain.Entidade;
using Microsoft.Extensions.Options;
using Moq;

namespace interview.generator.test.Infraestructure
{
    public class UsuarioTest
    {
        Mock<IUsuarioService> mock = new Mock<IUsuarioService>();

        [Fact]
        public async void ConsultarUsuario()
        {
            var usuarios = new List<Usuario>();
            usuarios.Add(new Usuario()
            {
                Cpf = "88888888",
                Id = 1,
                Nome = "Andre",
                PerfilId = "1"
            });


            mock.Setup(m => m.ListarUsuarios()).Returns(Task.FromResult<IEnumerable<Usuario>>(usuarios));
            var r = mock.Object.ListarUsuarios().Result;
            Assert.True(r.Any());
            
        }
    }
}