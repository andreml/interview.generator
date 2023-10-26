using interview.generator.domain.Entidade;
using interview.generator.domain.Enum;
using interview.generator.infraestructure.Repositorio;

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
                Id = Guid.NewGuid(),
                Nome = "Andre Teste 1",
                Perfil = Perfil.Candidato
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
                Id = Guid.NewGuid(),
                Nome = "Andre Teste 1",
                Perfil = Perfil.Candidato
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
            var result = await mockRepositorio.Object.ObterPorId(Guid.Empty);
            Assert.True(result is null);
        }
    }
}