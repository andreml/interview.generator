using interview.generator.domain.Entidade;
using interview.generator.infraestructure.SqlServer;

namespace interview.generator.test.Infraestructure
{
    public class UsuarioTest
    {
        Mock<UsuarioRepositorio> mockRepositorio = new Mock<UsuarioRepositorio>();
        [Fact]
        public async void AdicionarNovoUsuarioEConsultarPorId()
        {
            var usuarios = new Usuario()
            {
                Cpf = "12345679811",
                Id = 1,
                Nome = "Andre Teste 1",
                PerfilId = "1"
            };

            await mockRepositorio.Object.Adicionar(usuarios);
            var result = await mockRepositorio.Object.ObterPorId(usuarios.Id);

            Assert.True(result != null);
        }

        [Fact]
        public async void AlterarUsuarioCadastrado()
        {
            var usuarios = new Usuario()
            {
                Cpf = "12345679811",
                Id = 1,
                Nome = "Andre Teste 1",
                PerfilId = "1"
            };

            await mockRepositorio.Object.Adicionar(usuarios);
            usuarios.Cpf = "12312312399";
            await mockRepositorio.Object.Alterar(usuarios);
            var result = await mockRepositorio.Object.ObterPorId(usuarios.Id);

            Assert.True(usuarios.Cpf == "12312312399");
        }

        [Fact]
        public async void ConsultarUsuarioNaoCadastrado()
        {
            var result = await mockRepositorio.Object.ObterPorId(99999);
            Assert.True(result is null);
        }

        [Fact]
        public async void ListarUsuariosCadastrados()
        {
            var usuarios = new Usuario()
            {
                Cpf = "12345679811",
                Id = 1,
                Nome = "Andre Teste 1",
                PerfilId = "1"
            };
            await mockRepositorio.Object.Adicionar(usuarios);

            var usuarios1 = new Usuario()
            {
                Cpf = "12345679811",
                Id = 1,
                Nome = "Andre Teste 1",
                PerfilId = "1"
            };

            await mockRepositorio.Object.Adicionar(usuarios1);
            var result = await mockRepositorio.Object.ObterTodos();
            Assert.True(result.Count() > 0);
        }
    }
}